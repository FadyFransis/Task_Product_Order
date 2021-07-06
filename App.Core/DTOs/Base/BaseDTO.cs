using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.DTOs.Base
{
   public class BaseDTO
    {
        public long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string NameAr { get; set; }
        public virtual string DescriptionAr { get; set; }
    }
}
