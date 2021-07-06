using System.Collections.Generic;

namespace App.API.DTOs
{
    public class SearchResultDTO
    {
        public List<ProductDetailsDTO> Products { get; set; }
        public List<CategoryProductsCountDTO> Categories { get; set; }
        public List<BrandProductsCountDTO> Brands { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int SpecialDealsProducts { get; set; }
        public int NotSpecialDealsProducts { get; set; }

    }
}
