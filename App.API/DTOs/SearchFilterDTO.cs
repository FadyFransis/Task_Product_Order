using System.Collections.Generic;

namespace App.API.DTOs
{
    public class SearchFilterDTO
    {
        public string SearchText { get; set; }
        public List<int> Categories { get; set; }
        public List<int> Brands { get; set; }
        public bool IsBestSelling { get; set; }
        public bool IsSpecialDeals { get; set; }
        public bool NotSpecialDeals { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string Sort { get; set; }
    }
}
