using Microsoft.AspNetCore.Mvc;
using E_Shopper.Repository;
using Microsoft.EntityFrameworkCore;
using E_Shopper.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Shopper.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly EShopperDbContext _context;
        public OrderController(EShopperDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Detail(int orderid)
        {
            var order = _context.Orders.Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .FirstOrDefault(o => o.Id == orderid);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        public async Task<IActionResult> UpdateStatus(int orderid)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderid);
            if (order == null)
            {
                return NotFound();
            }
            try
            {
                if(order.Status == 0)
                    order.Status = 1;
                else
                    order.Status = 0;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                TempData["success"] = "Cập nhật trạng thái đơn hàng thành công.";
                return RedirectToAction("Index","Order",new {area = "Admin"});
            }
            catch (Exception ex)
            {
                TempData["error"] = "Cập nhật trạng thái đơn hàng thất bại.";
                return RedirectToAction("Index", "Order", new { area = "Admin" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int orderid)
        {
            var order = _context.Orders.Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .FirstOrDefault(o => o.Id == orderid);
            if (order == null)
            {
                return NotFound();
            }
            if(order.Status == 1)
            {
                TempData["error"] = "Không thể xóa đơn hàng đã được giao.";
                return RedirectToAction("Index", "Order", new { area = "Admin" });
            }
            try
            {
                foreach(var orderDetail in order.OrderDetails)
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
                return RedirectToAction("Index", "Order", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                TempData["error"] = "Không thể xóa đơn hàng này do gặp một số lỗi.";
                return RedirectToAction("Index", "Order", new { area = "Admin" });
            }
        }
    }
}
