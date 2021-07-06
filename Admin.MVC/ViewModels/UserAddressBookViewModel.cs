using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class UserAddressBookViewModel
    {

        public long Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Address Type")]
        public string Title { get; set; }

        [Display(Name = "Area")]
        public long AreaId { get; set; }
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }
        [Display(Name = "Area Name in Arabic")]
        public string AreaNameAr { get; set; }
        [Display(Name = "Address Description")]
        public string Address { get; set; }
        [Display(Name = "Additional Address")]
        public string AdditionalAddress { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }
        [Display(Name = "Is Default Address")]
        public bool IsDefaultAddress { get; set; }
    }
}
