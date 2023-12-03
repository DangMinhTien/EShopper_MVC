using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Views.Shared.Components.BrandsSideBar
{
	public class BrandSideBarView
	{
		public List<BrandModel> Brands { get; set; }
		public BrandModel Brand { get; set; }
	}
	public class BrandsSideBar : ViewComponent
	{
		private readonly EShopperDbContext _context;

		public BrandsSideBar(EShopperDbContext context)
        {
            _context = context;
        }
		public async Task<IViewComponentResult> InvokeAsync(BrandModel brand = null)
		{
			var brands = await _context.Brands.Include(b => b.Products).ToListAsync();

			return View(new BrandSideBarView
			{
				Brands = brands,
				Brand = brand
			});
		}
    }
}
