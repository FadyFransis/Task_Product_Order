using System.ComponentModel.DataAnnotations;

namespace App.API.DTOs
{
    public class LoginDTO
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
