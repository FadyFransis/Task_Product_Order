using System.ComponentModel.DataAnnotations;

namespace App.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "First name should be letters only")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "Last name should be letters only")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 character")]
        public string Password { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 character")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

    }
}
