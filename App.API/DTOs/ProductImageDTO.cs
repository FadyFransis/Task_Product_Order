using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.DTOs
{
    public class ProductImageDTO : BaseDTO
    {
        public long ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}
