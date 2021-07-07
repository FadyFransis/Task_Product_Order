namespace Admin.MVC.ViewModels
{
    public class BrandViewModel : BaseLookupViewModel
    {
        public string ImageUrl { get; set; }
    }
    public class BrandProductsCountViewModel : BrandViewModel
    {
        public int ProductsCount { get; set; }
        public bool Selected { get; set; }
    }
}
