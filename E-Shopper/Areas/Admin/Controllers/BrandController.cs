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
using Microsoft.AspNetCore.Authorization;

namespace E_Shopper.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly EShopperDbContext _context;
        private List<StatusModel> lststatus;

        public BrandController(EShopperDbContext context)
        {
            _context = context;
            lststatus = new List<StatusModel>
            {
                new StatusModel(1,"Hiện"),
                new StatusModel(0,"Ẩn")
            };
        }

        // GET: Admin/Brand
        public async Task<IActionResult> Index()
        {
              return View(await _context.Brands.ToListAsync());
        }

        // GET: Admin/Brand/Create
        public IActionResult Create()
        {
            ViewBag.Status = new SelectList(lststatus, "Id", "Name");
            return View();
        }

        // POST: Admin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Status")] BrandModel brandModel)
        {
            ViewBag.Status = new SelectList(lststatus, "Id", "Name",brandModel.Status);
            if (ModelState.IsValid)
            {
                var Slug = brandModel.Name.Replace(" ", "-");
                if (_context.Brands.Any(c => c.Slug == Slug))
                {
                    ModelState.AddModelError(string.Empty, "Đã có một thương hiệu có tên giống với tên bạn vừa nhập.");
                    TempData["error"] = "Đã có một thương hiệu có tên giống với tên bạn vừa nhập.";
                    return View(brandModel);
                }
                try
                {
                    brandModel.Slug = Slug;
                    _context.Add(brandModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Bạn đã thêm môt thương hiệu thành công";
                    return RedirectToAction("Index","Brand",new {area = "Admin"});
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            TempData["error"] = "Có lỗi khi thêm.";
            return View(brandModel);
        }

        // GET: Admin/Brand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brandModel = await _context.Brands.FindAsync(id);
            if (brandModel == null)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(lststatus, "Id", "Name", brandModel.Status);
            return View(brandModel);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status")] BrandModel brandModel)
        {
            if (id != brandModel.Id)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(lststatus, "Id", "Name", brandModel.Status);
            if (ModelState.IsValid)
            {
                var Slug = brandModel.Name.Replace(" ", "-");
                if (_context.Brands.Any(c => c.Slug == Slug && c.Id != id))
                {
                    ModelState.AddModelError(string.Empty, "Đã có một thương hiệu có tên giống với tên bạn vừa nhập.");
                    TempData["error"] = "Đã có một thương hiệu có tên giống với tên bạn vừa nhập.";
                    return View(brandModel);
                }
                try
                {
                    brandModel.Slug = Slug;
                    _context.Update(brandModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = $"Bạn đã sửa danh mục có mã {id} thành công";
                    return RedirectToAction("Index", "Brand", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    
                    return BadRequest(ex.Message);
                }
            }
            TempData["error"] = "Có lỗi khi sửa.";
            return View(brandModel);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'EShopperDbContext.Brands'  is null.");
            }
            var brandModel = await _context.Brands.FindAsync(id);
            if (brandModel != null)
            {
                if (_context.Products.Any(p => p.BrandId == id))
                {
                    TempData["error"] = "Không xóa được thương hiệu này vì có sản phẩm thuộc danh mục này.";
                    return RedirectToAction("Index", "Brand", new { area = "Admin" });
                }
                try
                {
                    _context.Brands.Remove(brandModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Bạn đã xóa thương hiệu này thành công";
                    return RedirectToAction("Index", "Brand", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

        private bool BrandModelExists(int id)
        {
          return _context.Brands.Any(e => e.Id == id);
        }
    }
}
