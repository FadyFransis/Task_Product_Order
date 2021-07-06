using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class SliderViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title Arabic")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Description Arabic")]
        public string DescriptionAr { get; set; }

        [Required(ErrorMessage = "ImageUrl Required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Button Url")]
        [Required(ErrorMessage = "Button Url Required")]
        public string ButtonUrl { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Button Text")]
        public string ButtonText { get; set; }    
        
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Button Text Arabic")]
        public string ButtonTextAr { get; set; }


        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
    }
}
