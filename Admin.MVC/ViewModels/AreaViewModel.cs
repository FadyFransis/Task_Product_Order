using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class AreaViewModel : BaseNameViewModel
    {
        [Required(ErrorMessage = "Required")]
        public long CityId { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        [Display(Name = "City Name Arabic")]
        public string CityNameAr { get; set; }
        [Display(Name = "Delivery Fees")]
        [Required(ErrorMessage = "Required")]
        public decimal DeliveryFees { get; set; }

    }
}
