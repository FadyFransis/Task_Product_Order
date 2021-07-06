using System;

namespace Admin.MVC.ViewModels
{
    public class ProductRatesViewModel : BaseViewModel
    {
        public long ProductId { get; set; }
        public string UserId { get; set; }
        public double Rate { get; set; }
        public string Experience { get; set; }
        public DateTime CreationDate { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
