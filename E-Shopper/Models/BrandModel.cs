using System.ComponentModel.DataAnnotations;

namespace E_Shopper.Models
{
	public class BrandModel
	{
		[Key]
		[Display(Name = "Mã")]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Tên thương hiệu")]
		[MaxLength(100, ErrorMessage = "{0} yêu cầu nhập tối đa {1} ký tự")]
		public string Name { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Mô tả")]
        [MaxLength(250, ErrorMessage = "{0} yêu cầu nhập tối đa {1} ký tự")]
		public string Description { get; set; }
		public string Slug { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Trạng thái")]
		public int Status { get; set; }
		public IEnumerable<ProductModel> Products { get; set; }
	}
}
