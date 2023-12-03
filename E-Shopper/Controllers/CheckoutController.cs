using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace E_Shopper.Controllers
{
	[Authorize]
	public class CheckoutController : Controller
	{
		private readonly EShopperDbContext _context;
		private readonly UserManager<AppUserModel> _userManager;
		private readonly SignInManager<AppUserModel> _signInManager;
		public CheckoutController(EShopperDbContext context,
			UserManager<AppUserModel> userManager,
			SignInManager<AppUserModel> signInManager)
        {
            _context = context;
			_userManager = userManager;
			_signInManager = signInManager;
        }
		public class InputModel
		{
			[Required(ErrorMessage = "{0} bắt buộc phải nhập")]
			[Display(Name = "Tên người đặt hàng")]
			public string Name { get; set; }
			[Required(ErrorMessage = "{0} bắt buộc phải nhập")]
			[Display(Name = "Số điện thoại người đặt hàng")]
			[DataType(DataType.PhoneNumber)]
			public string PhoneNumber { get; set; }
			[Display(Name = "Địa chỉ nhận hàng")]
			public string Address { get; set; }
		}
        public async Task<IActionResult> Index()
		{
			return View(new InputModel());
		}
		[HttpPost]
		public async Task<IActionResult> CreateOrder(InputModel inputModel)
		{
			string pattern = @"^\d{10}$";
			if (!Regex.IsMatch(inputModel.PhoneNumber, pattern))
				ModelState.AddModelError(string.Empty, "Số điện thoại không hợp lệ");
			if (ModelState.IsValid)
			{
				List<CartItemModel> cartItem = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				if(cartItem.Count() < 0)
				{
					TempData["error"] = "Đặt hàng thất bại vì chưa có sản phẩm nào trong giỏ hàng";
					return RedirectToAction("Index", "Cart");
				}
				if (!_signInManager.IsSignedIn(User))
				{
					TempData["error"] = "Đặt hàng thất bại yêu cầu bạn đăng nhập";
					return RedirectToAction("Index", "Cart");
				}
				AppUserModel user = await _userManager.GetUserAsync(User);
				if (user == null)
				{
					return NotFound();
				}
				try
				{
					OrderModel order = new OrderModel()
					{
						OrderCode = Guid.NewGuid().ToString(),
						Name = inputModel.Name,
						Address = inputModel.Address ?? user.Address,
						CreatedDate = DateTime.Now,
						Status = 0,
						Quantity = cartItem.Sum(c => c.Quantity),
						TotalPrice = cartItem.Sum(c => c.Total),
						PhoneNumber = inputModel.PhoneNumber,
						UserId = user.Id,
					};
					List<OrderDetail> orderDetails = new List<OrderDetail>();
					foreach (CartItemModel item in cartItem)
					{
						var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
						if (product == null)
						{
							return NotFound();
						}
						if(product.Quantity < item.Quantity)
						{
							TempData["error"] = $"Đặt hàng thất bại do số lượng sản phẩm {product.Name} không còn";
							return RedirectToAction("Index", "Cart");
						}
						orderDetails.Add(new OrderDetail()
						{
							Order = order,
							Product = product,
							Quantity = item.Quantity,
							Price = item.Price,
						});
						product.Quantity -= item.Quantity;
						_context.Products.Update(product);
					}
					await _context.Orders.AddAsync(order);
					await _context.OrderDetails.AddRangeAsync(orderDetails);
					await _context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					TempData["error"] = $"Đặt hàng thất bại";
					return RedirectToAction("Index", "Cart");
				}
				TempData["success"] = "Đặt hàng thành công";
				return RedirectToAction("Index", "Cart");
			}
			return View("Index",inputModel);
		}
		
	}
}
