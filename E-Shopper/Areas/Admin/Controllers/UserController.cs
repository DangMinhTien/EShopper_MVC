using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_Shopper.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly EShopperDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanage;
        private readonly UserManager<AppUserModel> _usermanage;
        public UserController(RoleManager<IdentityRole> rolemanage,
            EShopperDbContext context,
            UserManager<AppUserModel> usermanage)
        {
            _context = context;
            _rolemanage = rolemanage;
            _usermanage = usermanage;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _usermanage.Users.ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> AddRole(string userid)
        {
            if(userid == null)
            {
                return NotFound();
            }
            var user = await _usermanage.FindByIdAsync(userid);
            if(user == null)
            {
                return NotFound();
            }
            List<string> rolesOfUser = (await _usermanage.GetRolesAsync(user)).ToList<string>();
            List<string> roles = _rolemanage.Roles.Select(r => r.Name).ToList();
            ViewBag.Role = new MultiSelectList(roles, rolesOfUser);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string userid, List<string> selectRole)
        {
            if(userid == null)
            {
                return NotFound();
            }
            var user = await _usermanage.FindByIdAsync(userid);

            if (user == null)
            {
                return NotFound();
            }
            List<string> rolesOfUser = (await _usermanage.GetRolesAsync(user)).ToList<string>();
            List<string> roles = _rolemanage.Roles.Select(r => r.Name).ToList();
            ViewBag.Role = new MultiSelectList(roles, rolesOfUser);
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var OldRoleNames = (await _usermanage.GetRolesAsync(user)).ToArray<string>();
            var deleteRole = OldRoleNames.Where(r => !selectRole.Contains(r));
            var addRoles = selectRole.Where(r => !OldRoleNames.Contains(r));
            var resultdelete = await _usermanage.RemoveFromRolesAsync(user, deleteRole);
            if (!resultdelete.Succeeded)
            {
                resultdelete.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                });
                return View(user);
            }
            var resultAdd = await _usermanage.AddToRolesAsync(user, addRoles);
            if (!resultAdd.Succeeded)
            {
                resultAdd.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                });
                return View(user);
            }
            TempData["success"] = $"Gán vai trò thành công cho người dùng {user.UserName}";

            return RedirectToAction("Index");
        }
        public class InputChangePassword
        {
            public AppUserModel user { get; set; }
            [MinLength(6, ErrorMessage = "{0} tối thiểu phải có {1} ký tự")]
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} bắt buộc phải nhập.")]
            public string Password { get; set; }
            [Compare("Password", ErrorMessage = "{0} phải khớp với Mật khẩu")]
            [Display(Name = "Xác nhận mật khẩu")]
            [Required(ErrorMessage = "{0} bắt buộc phải nhập.")]
            public string ConfirmPassword { get; set; }
        }
        public async Task<IActionResult> ChangePassword(string userid)
        {
            if (userid == null)
            {
                return NotFound();
            }
            var user = await _usermanage.FindByIdAsync(userid);
            if(user == null)
            {
                return NotFound();
            }
            return View(new InputChangePassword() { user = user });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(InputChangePassword inputChangePassword, string userid)
        {
            if (userid == null)
            {
                return NotFound();
            }
            var user = await _usermanage.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                IdentityResult reusultRemove = await _usermanage.RemovePasswordAsync(user);
                if (reusultRemove.Succeeded)
                {
                    IdentityResult resultAdd = await _usermanage.AddPasswordAsync(user, inputChangePassword.Password);
                    if(resultAdd.Succeeded)
                    {
                        TempData["success"] = $"Đổi mật khẩu cho tài khoản {user.UserName} thành công";
                        return RedirectToAction("Index");
                    }
                    foreach (var error in resultAdd.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    TempData["error"] = "Đổi mật khẩu thất bại.";
                }
                foreach(var error in  reusultRemove.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["error"] = "Đổi mật khẩu thất bại.";
            }
            return View(new InputChangePassword() { user = user });
        }
    }
}
