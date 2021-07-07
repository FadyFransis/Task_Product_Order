using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required")]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required")]
        public string NewPassword { get; set; }
    }
}
