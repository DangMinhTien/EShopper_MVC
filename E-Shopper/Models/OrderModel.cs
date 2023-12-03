using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shopper.Models
{
	public class OrderModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string OrderCode { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime CreatedDate { get; set; }
		[Required]
		public int Status { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public decimal TotalPrice { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public AppUserModel User { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }

	}
}
