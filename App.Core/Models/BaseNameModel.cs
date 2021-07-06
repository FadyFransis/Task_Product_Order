using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Models
{
    public class BaseNameModel : BaseModel
    {
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string NameAr { get; set; }
    }
}
