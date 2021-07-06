using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class BaseLookupViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [Required (ErrorMessage ="Required")]
        public  string Name { get; set; }
        [Display(Name = "Description")]
        public  string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Name Arabic")]
        public  string NameAr { get; set; }
        [Display(Name = "Description Arabic")]
        public  string DescriptionAr { get; set; }
    }
}
