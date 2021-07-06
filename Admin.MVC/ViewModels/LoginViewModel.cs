using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required *")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
