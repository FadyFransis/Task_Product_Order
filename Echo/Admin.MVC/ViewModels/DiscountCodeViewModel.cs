using App.Core.Entities.Base;
using App.MVC.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    [DiscountValidation(ErrorMessage = "Discount Amount should be less than 100 if you select Percentage as Discount Type")]
    public class DiscountCodeViewModel : BaseLookupViewModel
    {
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Required")]
        public decimal Amount { get; set; }
        [Display(Name = "Discount Type")]
        public DiscountType DiscountType { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Required")]
        public string Code { get; set; }
        [Display(Name = "Expiration Date")]
        [Required(ErrorMessage = "Required")]
        public DateTime ExpirationDate { get; set; }
    }
}
