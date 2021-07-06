using App.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class AppSettingViewModel : BaseViewModel
    {
        [Display(Name = "Key")]
        public AppSettingKey Key { get; set; }
        
        [Display(Name = "Content")]
        public string Value { get; set; }
        
        [Display(Name = "Content Arabic")]
        public string ValueAr { get; set; }
    }
}
