using System.Collections.Generic;

namespace App.API.DTOs
{
    public class ProductBaseDTO : BaseLookupDTO
    {
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameAr { get; set; }
        public string BrandName { get; set; }
        public string BrandNameAr { get; set; }
        public bool IsTodayDeal { get; set; }
        public bool IsBestSelling { get; set; }
        public decimal Price { get; set; }
        public decimal EndPrice { get; set; }
        public string Image{ get; set; }
        public bool InMyFavouriteList { get; set; }
        public long ProductFavoriteId { get; set; }
        public int Stock { get; set; }
    }
    public class ProductDetailsDTO : ProductBaseDTO
    {
        
        public int RatesCount { get; set; }
        public double TotalRates { get; set; }
        public bool IsBoughtBefore { get; set; }
        public bool IsRatedBefore { get; set; }
        public bool IsInOrder { get; set; }

        public List<ProductImageDTO> ProductImages { get; set; }
        public List<ProductSizeDTO> ProductSizes { get; set; }
       // public List<ProductFavoriteDTO> ProductFavorites { get; set; }
        public List<ProductRateLookupDTO> ProductRates { get; set; }
    }
   
}
