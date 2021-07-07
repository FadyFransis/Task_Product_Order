using System.ComponentModel.DataAnnotations;

namespace App.API.DTOs
{
    public class SocialRegisterDTO
    {
        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "First name should be letters only")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "Last name should be letters only")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        public string SocialLoginId { get; set; }
    }
}
