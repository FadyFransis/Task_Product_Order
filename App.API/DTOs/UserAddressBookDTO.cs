using System.ComponentModel.DataAnnotations;

namespace App.API.DTOs
{
    public class UserAddressBookDTO : BaseDTO
    {
        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "Full Name should be letters only")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string PostalCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool IsDefaultAddress { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public long AreaId { get; set; }
        
    }
    public class UserAddressBookLookupDTO : UserAddressBookDTO
    {
        public int DeliveryFees { get; set; }
        public string AreaName { get; set; }
        public string AreaNameAr { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public string CityNameAr { get; set; }
    }
}
