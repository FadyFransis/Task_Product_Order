namespace App.API.DTOs
{
    public class ProductsCartDTO : BaseDTO
    {
        public long ProductSizeId { get; set; }
        public int Quantity { get; set; }
    }
    public class ProductsCartLookupDTO : ProductsCartDTO
    {
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductSizeName { get; set; }
        public string ProductSizeNameAr { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
        public decimal TotalPrice { get; set; }
        public long ProductId { get; set; }
        public int Stock{ get; set; }
    }
}
