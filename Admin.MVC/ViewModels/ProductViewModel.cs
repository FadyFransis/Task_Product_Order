using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class ProductViewModel : BaseLookupViewModel
    {
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        [Display(Name = "Start Price")]
        public int Price { get; set; }
        [Display(Name = "End Price")]
        public int EndPrice { get; set; }
        [Display(Name = "Is Today Deal")]
        public bool IsTodayDeal { get; set; }
        [Display(Name = "Is BestSelling Product")]
        public bool IsBestSelling { get; set; }
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Name Arabic")]
        public string CategoryNameAr { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Brand Name Arabic")]
        public string BrandNameAr { get; set; }
        public int RatesCount { get; set; }
        public double TotalRates { get; set; }
        public bool InMyFavouriteList { get; set; }
        public long ProductFavoriteId { get; set; }
        public bool IsBoughtBefore { get; set; }
        public bool IsRatedBefore { get; set; }
        [Display(Name = "Items Sold")]
        public int ItemsSold { get; set; }
        public DateTime CreationDate { get; set; }
        public string Image { get; set; }
        public bool IsInOrder { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
        //public List<ProductSizeViewModel> ProductSizes { get; set; }
        public List<ProductRatesViewModel> ProductRates { get; set; }
        public List<ProductFavoriteViewModel> ProductFavorites { get; set; }
    }
}
