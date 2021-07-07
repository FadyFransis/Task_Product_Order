using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class BaseViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }
    }
}
