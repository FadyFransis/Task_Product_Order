namespace Admin.MVC.ViewModels
{
    public class ProductFavoriteViewModel : BaseViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public int RatesCount { get; set; }
        public double TotalRates { get; set; }
        public string UserId { get; set; }
        public int Stock{ get; set; }
        public string ProductImage { get; set; }

    }
}
