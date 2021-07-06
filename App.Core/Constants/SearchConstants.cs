namespace App.Core.Constants
{
    public class SearchConstants
    {
        public enum SortTypes
        {

            AZ = 1,
            ZA = 2,
            LowestPrice = 3,
            HighPrice = 4
        }

        public const string CATEGORY_FILTER_NAME = "category";
        public const string Brand_FILTER_NAME = "brand";
        public const string QUERY_FILTER_NAME = "query";
        public const string PRICE_FILTER_NAME = "price";
        public const string SORT_FILTER_NAME = "sort";
        public const string SpecialDeals_FILTER_NAME = "isspecialdeals";
        public const string NotSpecialDeals_FILTER_NAME = "notspecialdeals";
        public const string BestSelling_FILTER_NAME = "isbestselling";

    }
}
