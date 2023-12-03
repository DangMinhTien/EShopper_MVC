using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shopper.Models
{
	[Table("Category")]
	public class CategoryModel
	{
		[Key]
		[Display(Name= "Mã")]
        public int Id { get; set; }
		[Required(ErrorMessage = "{0} bắt buộc phải nhập")]
		[Display(Name = "Tên danh mục")]
		[MaxLength(100, ErrorMessage = "{0} yêu cầu nhập tối đa {1} ký tự")]
		public string Name { get; set; }
        [Required(ErrorMessage = "{0} bắt buộc phải nhập")]
        [Display(Name = "Mô tả")]
        [MaxLength(250, ErrorMessage = "{0} yêu cầu nhập tối đa {1} ký tự")]
		public string Description { get; set; }
		public string Slug { get; set; }
        [Required(ErrorMessage = "{0} bắt buộc phải nhập")]
        [Display(Name = "trạng thái")]
        public int Status { get; set; }
    }
}
