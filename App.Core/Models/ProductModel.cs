using System;
using System.Collections.Generic;

namespace App.Core.Models
{
    public class ProductModel : BaseLookupModel
    {
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameAr { get; set; }
        public string BrandName { get; set; }
        public string BrandNameAr { get; set; }
        public bool IsTodayDeal { get; set; }
        public bool IsBestSelling { get; set; }
        public int Stock { get; set; }
        public int RatesCount { get; set; }
        public int FiveStarsCount { get; set; }
        public int FourStarsCount { get; set; }
        public int ThreeStarsCount { get; set; }
        public int TwoStarsCount { get; set; }
        public int OneStarCount { get; set; }
        public double TotalRates { get; set; }
        public bool InMyFavouriteList { get; set; }
        public long ProductFavoriteId { get; set; }
        public bool IsBoughtBefore { get; set; }
        public bool IsRatedBefore { get; set; }
        public decimal Price { get; set; }
        public decimal EndPrice { get; set; }
        public string Image{ get; set; }
        public bool IsInOrder { get; set; }
        public int ItemsSold { get; set; }

    }
}
