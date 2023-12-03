using E_Shopper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Controllers
{
	public class ProductController : Controller
	{
		private readonly ILogger<ProductController> _logger;
		private readonly EShopperDbContext _context;

		public ProductController(ILogger<ProductController> logger, EShopperDbContext context)
        {
            _logger = logger;
			_context = context;
        }
        public IActionResult Index()
		{
			return View();
		}
		
		public async Task<IActionResult> Detail(int? id)
		{
			if(id == null)
				return RedirectToAction("Index","Home");
			var product = _context.Products.Include(p => p.Brand)
										   .Include(p => p.Category)
										   .FirstOrDefault(x => x.Id == id);
			if(product == null)
				return NotFound();

			return View(product);
		}
	}
}
