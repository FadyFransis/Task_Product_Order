using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class BaseNameViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [Required (ErrorMessage ="Required")]
        public  string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Name Arabic")]
        public  string NameAr { get; set; }
    }
}
