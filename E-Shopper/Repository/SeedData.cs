using E_Shopper.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Repository
{
	public class SeedData
	{
		public static void SeedingData(EShopperDbContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any() && 
				!_context.Categories.Any() && 
				!_context.Brands.Any())
			{
				BrandModel apple = new BrandModel
				{
					Name = "apple",
					Slug = "apple",
					Description = "Apple là một hãng công nghệ lớn trên thế giới",
					Status = 1
				};
				BrandModel samsung = new BrandModel
				{
					Name = "samsung",
					Slug = "samsung",
					Description = "samsung là một hãng công nghệ lớn trên thế giới",
					Status = 1
				};
				CategoryModel dientu = new CategoryModel
				{
					Name = "đồ điện tử",
					Slug = "do-dien-tu",
					Description = "ở đây có đủ các mặt hàng điện tử nhé !",
					Status = 1
				};
				CategoryModel dogiadung = new CategoryModel
				{
					Name = "đồ gia dụng",
					Slug = "do-gia-dung",
					Description = "ở đây có đủ các mặt hàng đồ gia dụng nhé !",
					Status = 1
				};
				List<ProductModel> products = new List<ProductModel>
				{
					new ProductModel
					{
						Name = "Macbook Air 2 Pro",
						Description = "Laptop đẳng cấp nhất nhà táo",
						Status = 1,
						Price = 28900000,
						Slug = "macbook-air-2-pro",
						Image = "mac.jpg",
						Brand = apple,
						Category = dientu,
						Quantity = 100
					},
					new ProductModel
					{
						Name = "Samsung galaxy S22 Ultra",
						Description = "Điện thoại đẳng cấp nhất nhà S",
						Status = 1,
						Price = 23600000,
						Slug = "samsung-galaxy-s22-ultra",
						Image = "galaxys22.jpg",
						Brand = samsung,
						Category = dientu,
						Quantity = 100
					},
					new ProductModel
					{
						Name = "Tủ lạnh SamSung Cold Son",
						Description = "Tủ lạnh 800 lít mới nhất 2023 đến từ Hàn Quốc",
						Status = 1,
						Price = 20000000,
						Slug = "tu-lanh-samsung-cold-son",
						Image = "tulanhss.jpg",
						Brand = samsung,
						Category = dogiadung,
						Quantity = 100
					},
					new ProductModel
					{
						Name = "Nồi chiên không dầu SamSung G79",
						Description = "Nồi chiên không dầu hiện đại nhất của nhà S",
						Status = 1,
						Price = 790000,
						Slug = "noi-chien-khong-dau-samsung-g79",
						Image = "noichienss.jpg",
						Brand = samsung,
						Category = dogiadung,
						Quantity = 250
					}
				};
				_context.Products.AddRange(products);
				_context.SaveChanges();
			}
		}
	}
}
