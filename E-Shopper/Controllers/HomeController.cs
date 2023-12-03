using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Shopper.Controllers
{
    public class HomeController : Controller
    {
		private readonly EShopperDbContext _context;
		private readonly ILogger<HomeController> _logger;
        public HomeController(EShopperDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Brand)
                                            .Include(p => p.Category)
                                            .Where(p => p.Status != 0)
                                            .ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if(statuscode == 404)
            {
                return View("NotFound");
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}