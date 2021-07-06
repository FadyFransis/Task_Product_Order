using Admin.MVC.ViewModels;
using App.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace App.MVC.Helper
{
    public class DiscountValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var discount = (DiscountCodeViewModel)value;
            if ((discount.DiscountType == DiscountType.Percentage && discount.Amount <= 100 ) || discount.DiscountType==DiscountType.Amount)
            {
                return ValidationResult.Success;

            }
            else
            {
                return new ValidationResult("Discount Amount should be less than 100 if you select Percentage as Discount Type");
            }
        }
    }
}
