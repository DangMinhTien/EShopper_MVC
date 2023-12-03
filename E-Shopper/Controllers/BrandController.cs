using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Controllers
{
	public class BrandController : Controller
	{
		private readonly ILogger<BrandController> _logger;
		private readonly EShopperDbContext _context;

		public BrandController(ILogger<BrandController> logger, EShopperDbContext context)
        {
            _logger = logger;
			_context = context;
        }
        [Route("/brand/{Slug?}")]
        public async  Task<IActionResult> Index(string Slug = "")
		{
			BrandModel brand = _context.Brands.FirstOrDefault(b => b.Slug == Slug);
			if (brand == null)
			{
				return NotFound();
			}
			var productByBrand = _context.Products.Where(p => p.BrandId == brand.Id && p.Status != 0);
			ViewBag.Brand = brand;

			return View(await productByBrand.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
