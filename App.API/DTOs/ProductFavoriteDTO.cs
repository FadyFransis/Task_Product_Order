namespace App.API.DTOs
{
    public class ProductFavoriteDTO : BaseDTO
    {
        public string UserId { get; set; }
        public long ProductId { get; set; }
    }
    public class ProductFavoriteLookupDTO : ProductFavoriteDTO
    {
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public int Stock { get; set; }
        public string ProductImage { get; set; }
        public string CategoryName{ get; set; }
        public string CategoryNameAr { get; set; }
        public string BrandName { get; set; }
        public string BrandNameAr { get; set; }
        public decimal StartPrice { get; set; }

    }
}
