using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Models
{
    public class AppUserModel
    {
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "First name not in format")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "Last name not in format")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Activated")]
        public bool EmailConfirmed { get; set; }

        [Required]
        public string Roles { get; set; }
        public string Token { get; set; }
        public string ImageUrl { get; set; }
        public string SocialLoginId { get; set; }
        public DateTime DateAdded { get; set; }

    }
}