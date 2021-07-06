using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Entities.Base
{
    public class CommonBaseNameBusinessEntity : BaseEntity
    {
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public virtual string Name { get; set; }

        //  [StringLength(500, MinimumLength = 10)]
        public virtual string Description { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public virtual string NameAr { get; set; }

        //  [StringLength(500, MinimumLength = 10)]
        public virtual string DescriptionAr { get; set; }

    }
}
