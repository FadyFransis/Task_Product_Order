using System.ComponentModel.DataAnnotations;

namespace App.API.DTOs
{
    public class ActivateRegisterDTO
    {
        [Required]
        public string Email { get; set; }
        [Required (ErrorMessage ="The Activation Code is required")]
        public string ActivationCode { get; set; }
    }
}
