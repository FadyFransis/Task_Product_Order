using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Entities.Base
{
    public class BaseNameEntity : BaseEntity
    {
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public virtual string NameAr { get; set; }

    }
}
