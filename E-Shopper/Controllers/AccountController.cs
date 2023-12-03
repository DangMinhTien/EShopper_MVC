using E_Shopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Shopper.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<AppUserModel> _userManager;
		private readonly SignInManager<AppUserModel> _signInManager;
		private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<AppUserModel> userManager,
			SignInManager<AppUserModel> signInManager,
			ILogger<AccountController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public class InputRegister
        {
			[Required(ErrorMessage = "{0} không được để trống")]
			[EmailAddress(ErrorMessage = "Yêu cầu phải nhập đúng định dạng email")]
			public string Email { get; set; }
			[Required(ErrorMessage = "{0} không được để trống")]
			public string Address { get; set; }
			[Required(ErrorMessage = "{0} không được để trống")]
			public string Password { get; set; }
		}
		public class InputLogin
		{
			[Required(ErrorMessage = "{0} không được để trống")]
			public string UserName { get; set; }
			[Required(ErrorMessage = "{0} không được để trống")]
			public string Password { get; set; }
			public bool RememberMe { get; set; }
		}
        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Register(InputRegister userModel, string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ViewBag.ReturnUrl = returnUrl;
			if (ModelState.IsValid)
			{
				var userInEmail = await _userManager.FindByEmailAsync(userModel.Email);
				if(userInEmail != null)
				{
					ModelState.AddModelError(string.Empty, "Email này đã tồn tại");
					TempData["error"] = "Email bạn vừa nhập đã tồn tại";
					return View("Index");
				}
				AppUserModel user = new AppUserModel()
				{
					UserName = userModel.Email,
					Email = userModel.Email,
					Address = userModel.Address
				};
				IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);
				if (result.Succeeded)
				{
					_logger.LogInformation("Đã tạo user mới");
					await _signInManager.SignInAsync(user, isPersistent: false);
					TempData["success"] = "Bạn vừa đăng ký tài khoản thành công";
					return LocalRedirect(returnUrl);
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				TempData["error"] = "Có lỗi khi đăng ký tài khoản";
				return View("Index");
			}
			TempData["error"] = "Có lỗi khi đăng ký tài khoản";
			return View("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Login(InputLogin userModel, string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ViewBag.ReturnUrl = returnUrl;
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(userModel.UserName, userModel.Password, 
					userModel.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					_logger.LogInformation("Login thành công.");
					TempData["success"] = "Đăng nhập thành công";
					return LocalRedirect(returnUrl);
				}
				if (result.RequiresTwoFactor)
				{
					TempData["error"] = "Yêu cầu xác thực 2 bước";
					return RedirectToAction("Index", new { returnUrl = returnUrl });
				}
				if (result.IsLockedOut)
				{
					_logger.LogWarning("Tài khoản của bạn đã bị khóa.");
					TempData["error"] = "Tài khoản của bạn đã bị khóa";
					return RedirectToAction("Index", new { returnUrl = returnUrl });
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại sai username hoặc password.");
					TempData["error"] = "Tài khoản không tồn tại sai username hoặc password.";
					return View("Index");
				}

			}
			TempData["error"] = "Có lỗi khi đăng nhập tài khoản";
			return View("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Logout(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
			TempData["success"] = "Bạn vừa đăng xuất thành công";
            return LocalRedirect(returnUrl);
        }
		[Route("/accessdenied")]
		public async Task<IActionResult> AccessDenied()
		{
			return View();
		}

    }
}
