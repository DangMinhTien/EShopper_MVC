using E_Shopper.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Shopper.Repository
{
	public class EShopperDbContext : IdentityDbContext<AppUserModel>
	{
        public EShopperDbContext(DbContextOptions<EShopperDbContext> options) : base(options)
        {
            
        }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductModel> Products { get; set; }
		public DbSet<OrderModel> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryModel>(builder =>
			{
				builder.HasIndex(c => c.Slug).IsUnique();
			});
			modelBuilder.Entity<BrandModel>(builder =>
			{
				builder.HasIndex(b => b.Slug).IsUnique();
			});
			modelBuilder.Entity<ProductModel>(builder =>
			{
				builder.HasIndex(p => p.Slug).IsUnique();
			});

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
			modelBuilder.Entity<OrderModel>(builder =>
			{
				builder.HasIndex(o => o.OrderCode).IsUnique();
			});
			modelBuilder.Entity<OrderDetail>(builder =>
			{
				builder.HasKey(od => new {od.OrderId, od.ProductId});
			});
        }
	}
}
