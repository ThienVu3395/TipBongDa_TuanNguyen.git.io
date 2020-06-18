using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [MinLength(6, ErrorMessage = "{0} phải tối thiếu {1} ký tự.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và nhập lại mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

    }

    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tài khoản")]
        [StringLength(256, ErrorMessage = "{0} không được vượt quá {1} ký tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [MinLength(6, ErrorMessage = "{0} phải tối thiếu {1} ký tự")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và nhập lại mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn chưa xác định vai trò của người dùng này")]
        [Display(Name = "Vai trò")]
        public string RoleId { get; set; }

        [Display(Name = "Điểm số")]
        public int Point { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới")]
        [MinLength(6, ErrorMessage = "{0} phải tối thiếu {1} ký tự")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
