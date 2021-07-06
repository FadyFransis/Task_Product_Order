using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Required *")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required *")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Required *")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
