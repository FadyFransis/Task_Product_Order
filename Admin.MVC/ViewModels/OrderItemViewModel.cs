namespace Admin.MVC.ViewModels
{
    public class OrderItemViewModel : BaseViewModel
    {
        public long OrderId { get; set; }
        public long ProductSizeId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductSizeName { get; set; }
        public string ProductSizeNameAr { get; set; }
        public string ProductImage { get; set; }
    }
}
