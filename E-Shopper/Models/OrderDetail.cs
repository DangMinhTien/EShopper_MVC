using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shopper.Models
{
	public class OrderDetail
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public decimal Price { get; set; }
		[ForeignKey("OrderId")]
		public OrderModel Order { get; set; }
		[ForeignKey("ProductId")]
		public ProductModel Product { get; set; }
	}
}
