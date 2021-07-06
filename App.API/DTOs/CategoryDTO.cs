namespace App.API.DTOs
{
    public class CategoryDTO : BaseNameDTO
    {
        public string ImageUrl { get; set; }
        public bool HasChild { get; set; }
    }
    public class CategoryProductsCountDTO : CategoryDTO
    {
        public bool Selected { get; set; }
        public int ProductsCount { get; set; }
    }
}
