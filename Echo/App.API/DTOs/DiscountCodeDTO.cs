using App.Core.Entities.Base;
using System;

namespace App.API.DTOs
{
    public class DiscountCodeDTO : BaseLookupDTO
    {
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
