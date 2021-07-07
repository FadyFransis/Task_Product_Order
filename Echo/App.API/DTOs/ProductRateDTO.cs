using System;

namespace App.API.DTOs
{
    public class ProductRateDTO : BaseDTO
    {
        public long ProductId { get; set; }
        public double Rate { get; set; }
        public string Experience { get; set; }
    }
    public class ProductRateLookupDTO:ProductRateDTO
    {
        public DateTime CreationDate { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
