using System.ComponentModel.DataAnnotations;

namespace E_Shopper.Models
{
	public class UserModel
	{
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Tên đăng nhập/Email")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[EmailAddress(ErrorMessage = "Yêu cầu phải nhập đúng định dạng email")]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Địa chỉ")]
		public string Address { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; }
		[Required(ErrorMessage = "{0} không được để trống")]
		[Display(Name = "Xác nhận mật khẩu")]
		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
		public string ConfirmPassword { get; set; }
		public bool RememberMe { get; set; }
	}
}
