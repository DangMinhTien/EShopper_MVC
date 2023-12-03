using E_Shopper.Migrations;
using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Controllers
{
    [Authorize]
    public class MyOrderController : Controller
    {
        private readonly EShopperDbContext _context;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly SignInManager<AppUserModel> _signInManager;
        public MyOrderController(EShopperDbContext context,
            UserManager<AppUserModel> userManager,
            SignInManager<AppUserModel> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["error"] = "Bạn phải đăng nhập để xem đơn hàng của bạn";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            AppUserModel user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            List<OrderModel> orders = await _context.Orders.Where(o => o.UserId == user.Id).ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Detail(int orderid)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["error"] = "Bạn phải đăng nhập để xem đơn hàng của bạn";
                return RedirectToAction("Index");
            }
            AppUserModel user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var order = _context.Orders.Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .FirstOrDefault(o => o.Id == orderid);
            if (order == null)
            {
                return NotFound();
            }
            if(order.UserId != user.Id)
            {
                TempData["error"] = "Bạn không thể xem đơn hàng này";
                return RedirectToAction("Index");
            }
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int orderid)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                TempData["error"] = "Bạn phải đăng nhập để xem đơn hàng của bạn";
                return RedirectToAction("Index");
            }
            AppUserModel user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var order = _context.Orders.Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .FirstOrDefault(o => o.Id == orderid);
            if (order == null)
            {
                return NotFound();
            }
            if (order.UserId != user.Id)
            {
                TempData["error"] = "Bạn không thể xem đơn hàng này";
                return RedirectToAction("Index");
            }
            if (order.Status == 1)
            {
                TempData["error"] = "Không thể xóa đơn hàng đã được giao.";
                return RedirectToAction("Index");
            }
            try
            {
                foreach (var orderDetail in order.OrderDetails)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == orderDetail.ProductId);
                    if (product == null)
                        return NotFound();
                    product.Quantity += orderDetail.Quantity;
                    _context.Products.Update(product);
                }
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                TempData["success"] = "Bạn đã xóa thành công một đơn hàng.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Không thể xóa đơn hàng này do gặp một số lỗi.";
                return RedirectToAction("Index");
            }
        }
    }
}
