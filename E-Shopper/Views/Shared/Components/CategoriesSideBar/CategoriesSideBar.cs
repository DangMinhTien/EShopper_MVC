using E_Shopper.Models;
using E_Shopper.Repository;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopper.Views.Shared.Components.CategoriesSideBar
{
	public class CategorySideBarView
	{
		public List<CategoryModel> Categories { get; set; }
		public CategoryModel Category { get; set; }
	}
	public class CategoriesSideBar : ViewComponent
	{
		private readonly EShopperDbContext _context;

		public CategoriesSideBar(EShopperDbContext context)
        {
            _context = context;
        }
		public IViewComponentResult Invoke(CategoryModel category)
		{
			return View(new CategorySideBarView
			{
				Categories = _context.Categories.ToList(),
				Category = category
			});
		}
    }
}
