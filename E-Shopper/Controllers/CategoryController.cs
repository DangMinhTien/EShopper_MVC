using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Controllers
{
	public class CategoryController : Controller
	{
		private readonly EShopperDbContext _context;
		private readonly ILogger<CategoryController> _logger;

		public CategoryController(EShopperDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
			_logger = logger;
        }
		[Route("/category/{Slug?}")]
        public async Task<IActionResult> Index(string Slug)
		{
			CategoryModel cate = _context.Categories.FirstOrDefault(c => c.Slug == Slug);
			if (cate == null)
			{
				return NotFound();
			}
			var productByCategory = _context.Products.Where(p => p.CategoryId == cate.Id && p.Status != 0);
			ViewBag.Category = cate;
			return View(await productByCategory.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
