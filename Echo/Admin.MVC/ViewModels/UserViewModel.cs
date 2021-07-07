using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required *")]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "First name not in format")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Required *")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"([^0-9]*)$", ErrorMessage = "Last name not in format")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required *")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Required *")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Active")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Adding Date")]
        public DateTime DateAdded { get; set; }
        public string Roles { get; set; }
    }
}
