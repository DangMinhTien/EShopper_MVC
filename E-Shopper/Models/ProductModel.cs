using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shopper.Models
{
	[Table("Product")]
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Tên sản phẩm")]
		[MaxLength(100, ErrorMessage = "{0} yêu cầu nhập tối đa {1} ký tự")]
		public string Name { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Mô tả sản phẩm")]
		[MaxLength(250, ErrorMessage = "{0} yêu cầu nhập tối đa {1} ký tự")]
		public string Description { get; set; }
		public string Slug { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Trạng thái")]
		public int Status { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Giá bán")]
		[Range(0, double.MaxValue, ErrorMessage = "{0} phải có giá trị trong khoảng {1} đến {2}")]
		public decimal Price { get; set; }
		[Display(Name = "Ảnh sản phẩm")]
		public string Image {  get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Số lượng")]
		[Range(0, int.MaxValue, ErrorMessage = "{0} phải có giá trị trong khoảng {1} đến {2}")]
		public int Quantity { get; set; }
		[Display(Name = "Danh mục")]
		public int CategoryId { get; set; }
		[Display(Name = "Nhãn hàng")]
		public int BrandId { get; set; }
		[ForeignKey("CategoryId")]
		public CategoryModel Category { get; set; }
		[ForeignKey("BrandId")]
		public BrandModel Brand { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
	}
}
