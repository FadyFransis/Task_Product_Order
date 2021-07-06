namespace App.API.DTOs
{
    public class BrandDTO : BaseNameDTO
    {
        public string ImageUrl { get; set; }
    }

    public class BrandProductsCountDTO : BrandDTO
    {
        public bool Selected { get; set; }
        public int ProductsCount { get; set; }
    }
}
