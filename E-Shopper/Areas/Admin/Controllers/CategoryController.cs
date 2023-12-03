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
    public class CategoryController : Controller
    {
        private readonly EShopperDbContext _context;
        private List<StatusModel> lststatus;

        public CategoryController(EShopperDbContext context)
        {
            _context = context;
            lststatus = new List<StatusModel>
            {
                new StatusModel(1,"Hiện"),
                new StatusModel(0,"Ẩn")
            };
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index()
        {
              return View(await _context.Categories.ToListAsync());
        }


        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            ViewBag.Status = new SelectList(lststatus, "Id", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Status")] CategoryModel categoryModel)
        {
            ViewBag.Status = new SelectList(lststatus, "Id", "Name");
            if (ModelState.IsValid)
            {
                var Slug = categoryModel.Name.Replace(" ", "-");
                if(_context.Categories.Any(c => c.Slug == Slug))
                {
                    ModelState.AddModelError(string.Empty, "Đã có một danh mục có tên giống với tên bạn vừa nhập.");
                    TempData["error"] = "Đã có một danh mục có tên giống với tên bạn vừa nhập.";
                    return View(categoryModel);
                }
                try
                {
                    categoryModel.Slug = Slug;
                    _context.Add(categoryModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Bạn vừa thêm mới một danh mục.";
                    return RedirectToAction("Index","Category",new {area = "Admin"});

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            TempData["error"] = "Có một số lỗi khi thêm.";
            return View(categoryModel);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(lststatus, "Id", "Name", categoryModel.Status);
            return View(categoryModel);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status")] CategoryModel categoryModel)
        {
            ViewBag.Status = new SelectList(lststatus, "Id", "Name", categoryModel.Status);
            if (id != categoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var Slug = categoryModel.Name.Replace(" ", "-");
                if(_context.Categories.Any(c => c.Slug == Slug && c.Id != id))
                {
                    ModelState.AddModelError(string.Empty, "Đã có một danh mục có tên giống với tên bạn vừa nhập.");
                    TempData["error"] = "Đã có một danh mục có tên giống với tên bạn vừa nhập.";
                    return View(categoryModel);
                }
                try
                {
                    categoryModel.Slug = Slug;
                    _context.Update(categoryModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Bạn vừa sửa thành công danh mục sản phẩm có mã " + id;
                    return RedirectToAction("Index", "Category", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            TempData["error"] = "Có một số lỗi khi sửa danh mục.";
            return View(categoryModel);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'EShopperDbContext.Categories'  is null.");
            }
            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel != null)
            {
                if(_context.Products.Any(p => p.CategoryId == id))
                {
                    TempData["error"] = "Không xóa được danh mục này vì có sản phẩm thuộc danh mục này.";
                    return RedirectToAction("Index", "Category", new { area = "Admin" });
                }
                try
                {
                    _context.Categories.Remove(categoryModel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Bạn vừa xóa thành công một danh mục";
                    return RedirectToAction("Index", "Category", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

        private bool CategoryModelExists(int id)
        {
          return _context.Categories.Any(e => e.Id == id);
        }
    }
}
