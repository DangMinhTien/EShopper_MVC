using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Shopper.Models;
using E_Shopper.Repository;
using E_Shopper.Helper;
using E_Shopper.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace E_Shopper.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly EShopperDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private List<StatusModel> lststatus;
        public int Page_Size { get; set; } = 3;

        public ProductController(EShopperDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            lststatus = new List<StatusModel>
            {
                new StatusModel(1,"Hiện"),
                new StatusModel(0,"Ẩn")
            };
        }
        [HttpGet]
        // GET: Admin/Product
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            List<ProductModel> products;
           
            if (!string.IsNullOrEmpty(search))
            {
                products = await _context.Products.Include(p => p.Brand).Include(p => p.Category)
                    .Where(p => p.Name.Contains(search))
                    .OrderByDescending(p => p.Id).ToListAsync();
            }
            else
            {
                products = await _context.Products.Include(p => p.Brand).Include(p => p.Category).
                    OrderByDescending(p => p.Id).ToListAsync();
            }
            int Totalproduct = products.Count();
            int CountPage = (int)Math.Ceiling((double)Totalproduct / Page_Size);
            if (page < 0)
                page = 1;
            if(page > CountPage)
                page = CountPage;
            products = products.Skip((page - 1) * Page_Size).Take(Page_Size).ToList();
            ViewBag.currentPage = page;
            ViewBag.countPage = CountPage;
            ViewBag.search = search;
            return View(products);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["Status"] = new SelectList(lststatus, "Id", "Name");
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Status,Price,Quantity,CategoryId,BrandId")] ProductModel productModel,
            [Required(ErrorMessage = "Phải chọn file ảnh")][FileExtensionImage]IFormFile ImageFile)
        {
            ViewData["Status"] = new SelectList(lststatus,"Id","Name",productModel.Status);
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", productModel.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productModel.CategoryId);
            if (ModelState.IsValid)
            {
                productModel.Slug = productModel.Name.Replace(" ", "-");
                var productSlug = _context.Products.FirstOrDefault(p => p.Slug == productModel.Slug);
                if(productSlug != null)
                {
                    ModelState.AddModelError(string.Empty, "đã có một sản phẩm sử dụng tên bạn vừa nhập");
                    TempData["error"] = "đã có một sản phẩm sử dụng tên bạn vừa nhập";
                    return View(productModel);
                }
                if(ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) 
                        + Path.GetExtension(ImageFile.FileName);
                    string path = Path.Combine(_environment.WebRootPath, "images", "product-details", filename);
                    FileStream file = new FileStream(path, FileMode.Create);
                    await ImageFile.CopyToAsync(file);
                    file.Close();
                    productModel.Image = filename;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bạn phải chọn file ảnh");
                    TempData["error"] = "Bạn phải chọn file ảnh";
                    return View(productModel);
                }
                try
                {
                    _context.Add(productModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Bạn vừa thêm một sản phẩm thành công";
                    return RedirectToAction("Index","Product",new {area = "Admin"});
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Có một vài lỗi: " + ex.Message);
                    TempData["error"] = "Có một vài lỗi: " + ex.Message;
                    return View(productModel);
                }
            }
            TempData["error"] = "Có lỗi khi nhập";
            return View(productModel);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(lststatus, "Id", "Name", productModel.Status);
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", productModel.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productModel.CategoryId);
            return View(productModel);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status,Price,Image,Quantity,CategoryId,BrandId")] ProductModel productModel,
            [FileExtensionImage]IFormFile ImageFile)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(lststatus, "Id", "Name", productModel.Status);
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", productModel.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productModel.CategoryId);
            if (ModelState.IsValid)
            {
                var Slug = productModel.Name.Replace(" ", "-");
                productModel.Slug = Slug;
                if (_context.Products.Any(p => p.Slug == Slug && p.Id != id))
                {
                    ModelState.AddModelError(string.Empty, "đã có một sản phẩm sử dụng tên bạn vừa nhập");
                    TempData["error"] = "đã có một sản phẩm sử dụng tên bạn vừa nhập";
                    return View(productModel);
                }
                if(ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                        + Path.GetExtension(ImageFile.FileName);
                    string path = Path.Combine(_environment.WebRootPath, "images", "product-details", filename);
                    FileStream file = new FileStream(path, FileMode.Create);
                    await ImageFile.CopyToAsync(file);
                    file.Close();
                    productModel.Image = filename;
                }
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = $"Bạn vừa cập nhật sản phẩm có mã {id} thành công";
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi: " + ex.Message);
                    TempData["error"] = "Có lỗi: " + ex.Message;
                    return View(productModel);
                }
            }
            TempData["error"] = "Có lỗi một vài lỗi khi sửa.";
            return View(productModel);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EShopperDbContext.Products'  is null.");
            }
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            try
            {
                string ImagePath = Path.Combine(_environment.WebRootPath, "images", "product-details", productModel.Image);
                if (System.IO.File.Exists(ImagePath))
                {
                    System.IO.File.Delete(ImagePath);
                }
                _context.Products.Remove(productModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Bạn vừa xóa sản phẩm thành công";
                return RedirectToAction("Index", "Product", new { area = "Admin" });

            }
            catch (Exception ex)
            {
                TempData["error"] = "Có lỗi khi xóa";
                return RedirectToAction("Delete","Product",new {id = productModel.Id, area = "Admin"});
            }
        }

        private bool ProductModelExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
