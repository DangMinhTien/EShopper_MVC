using E_Shopper.Models;
using E_Shopper.Models.ViewModel;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopper.Controllers
{
	public class CartController : Controller
	{
		public readonly EShopperDbContext _context;
        public CartController(EShopperDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			List<CartItemModel> cartItem = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new CartItemViewModel()
			{
				CartItem = cartItem,
				GrandTotal = cartItem.Sum(c => c.Total),
			};
			return View(cartVM);
		}
		
		public async Task<IActionResult> Add(int id, int quantity = 1)
		{
			ProductModel product = _context.Products.FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel productIncart = cartItems.FirstOrDefault(c => c.ProductId == id);
			if (productIncart != null)
			{
				if (productIncart.Quantity + quantity < 1)
				{
					TempData["error"] = "Yêu cầu chọn số lượng từ 1 trở lên";
					return Redirect(Request.Headers["Referer"].ToString());
				}
				if (productIncart.Quantity + quantity > product.Quantity)
				{
					TempData["error"] = "Số lượng không đủ để đặt hàng";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
				productIncart.Quantity += quantity;
			}
			else
			{
				if(quantity > product.Quantity)
				{
					TempData["error"] = "Số lượng không đủ để đặt hàng";
					return Redirect(Request.Headers["Referer"].ToString());
				}
				if(quantity < 1)
				{
					TempData["error"] = "Yêu cầu chọn số lượng từ 1 trở lên";
					return Redirect(Request.Headers["Referer"].ToString());
				}
				CartItemModel cartItem = new CartItemModel(product, quantity);
				cartItems.Add(cartItem);
			}
			HttpContext.Session.SetJson("Cart", cartItems);
			TempData["success"] = $"Bạn vừa thêm {product.Name} vào giỏ hàng. ";
			TempData["url"] = Url.Action("Index", "Cart");
            return Redirect(Request.Headers["Referer"].ToString());
		}
		public async Task<IActionResult> Increase(int id)
		{
			ProductModel product = _context.Products.FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel productIncart = cartItems.FirstOrDefault(c => c.ProductId == id);
			if (productIncart != null)
			{
				if(productIncart.Quantity < product.Quantity)
				{
					productIncart.Quantity += 1;
					TempData["success"] = $"Bạn vừa tăng số lượng {product.Name} trong giỏ hàng lên {productIncart.Quantity}. ";
				}
			}
			HttpContext.Session.SetJson("Cart", cartItems);
            return RedirectToAction("Index");
		}
		public async Task<IActionResult> Decrease(int id)
		{
			ProductModel product = _context.Products.FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel productIncart = cartItems.FirstOrDefault(c => c.ProductId == id);
			if (productIncart != null)
			{
				if(productIncart.Quantity > 1)
				{
					productIncart.Quantity -= 1;
                    TempData["success"] = $"Bạn vừa giảm số lượng {product.Name} trong giỏ hàng giảm {productIncart.Quantity}. ";
                }
			}
			HttpContext.Session.SetJson("Cart", cartItems);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int id)
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel productIncart = cartItems.FirstOrDefault(c => c.ProductId == id);
			if (productIncart != null)
			{
				cartItems.RemoveAll(c => c.ProductId == id);
				HttpContext.Session.SetJson("Cart", cartItems);
				TempData["success"] = $"Bạn vừa xóa {productIncart.ProductName} trong giỏ hàng. ";
			}
            return RedirectToAction("Index");
		}
		public IActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}
	}
}
