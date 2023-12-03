using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace E_Shopper.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly EShopperDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanage;
        private readonly UserManager<AppUserModel> _usermanage;

        public RoleController(RoleManager<IdentityRole> rolemanage, 
            EShopperDbContext context,
            UserManager<AppUserModel> usermanage)
        {
            _context = context;
            _rolemanage = rolemanage;
            _usermanage = usermanage;
        }
        public class InputRoleModel
        {
            public string Id { get; set; }
            [Display(Name = "Tên quyền")]
            [Required(ErrorMessage = "{0} bắt buộc phải nhập")]
            public string Name { get; set; }
        }
        public async Task<IActionResult> Index()
        {
            var lstRole = await _rolemanage.Roles.ToListAsync();
            return View(lstRole);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new InputRoleModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(InputRoleModel roleModel)
        {
            if(ModelState.IsValid)
            {
                var newRole = new IdentityRole(roleModel.Name);
                var result = await _rolemanage.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    TempData["success"] = "Bạn vừa tạo quyền mới.";
                    return RedirectToAction("Index");
                }
                else
                {
                    result.Errors.ToList().ForEach(error =>
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    });
                    TempData["error"] = "Tạo quyền thất bại.";
                    return View(new InputRoleModel());
                }
            }
            TempData["error"] = "Tạo quyền thất bại.";
            return View(new InputRoleModel());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string roleid)
        {
            if (roleid == null)
            {
                return NotFound();
            }

            var role = await _rolemanage.FindByIdAsync(roleid);
            if (role != null)
            {
                InputRoleModel roleModel = new InputRoleModel()
                {
                    Name = role.Name,
                    Id = role.Id
                };
                return View(roleModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string roleid, InputRoleModel roleModel)
        {
            if (roleid != roleModel.Id || roleid == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var role = await _rolemanage.FindByIdAsync(roleid);
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = roleModel.Name;
                var result = await _rolemanage.UpdateAsync(role);
                if(result.Succeeded)
                {
                    TempData["success"] = "Bạn vừa sửa quyền thành công.";
                    return RedirectToAction("Index");
                }
                else
                {
                    result.Errors.ToList().ForEach(error =>
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    });
                    TempData["error"] = "Sửa quyền thất bại.";
                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "Sửa quyền thất bại.";
            return View(roleModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string roleid)
        {
            if(roleid == null)
            {
                return NotFound();
            }
            var role = await _rolemanage.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound();
            }
            var result = await _rolemanage.DeleteAsync(role);
            if(result.Succeeded)
            {
                TempData["success"] = "Bạn vừa xóa quyền thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Không thể xóa quyền vừa chọn";
                return RedirectToAction("Index");
            }

        }
    }
}
