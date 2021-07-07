using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.MVC.ViewModels
{
    public class CategoryViewModel : BaseLookupViewModel
    {
        [Display(Name = "Parent Category")]
        public long? ParentCategoryId { get; set; }
        [Display(Name = "Parent Category")]
        public string ParentCategoryName { get; set; }
        [Display(Name = "Parent Category In Arabic")]
        public string ParentCategoryNameAr { get; set; }
        public bool HasChild { get; set; }
        public string ImageUrl { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; }
    }
}
