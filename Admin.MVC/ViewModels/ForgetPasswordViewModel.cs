using System.ComponentModel.DataAnnotations;
namespace Admin.MVC.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Required *")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
