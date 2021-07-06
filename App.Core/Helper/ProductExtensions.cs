using App.Core.Entities;
using System.Linq;
using App.Core.Models;
using App.Core.Constants;
using System;
using System.Collections.Generic;

namespace App.Core.Helper
{
    public static class ProductExtensions
    {
     

        public static IQueryable<ProductModel> GetProductsModel(this IQueryable<Product> products)
        {

            UploadConstants uploadConstants = new UploadConstants();
            var productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
              
                Stock = x.Stock,
                TotalRates = 0,
                RatesCount = 0,

            });
            //productModels.All(c => { c.TotalRates = GetProductTotalRates(c); return true; });
            return productModels;

        }

        public static List<ProductModel> GetProductsModel(this List<Product> products)
        {

            UploadConstants uploadConstants = new UploadConstants();
            var productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                TotalRates = 0,
                RatesCount = 0,

            });
            //productModels.All(c => { c.TotalRates = GetProductTotalRates(c); return true; });
            return productModels.ToList();

        }


        public static IQueryable<ProductModel> GetProductsBaseModels(this IQueryable<Product> products)
        {
            UploadConstants uploadConstants = new UploadConstants();
            var productModels = products.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,

                RatesCount = 0,
                TotalRates = 0
            });
                

            return productModels;

        }


        public static ProductModel GetProductModel(this Product product)
        {
            try
            {
                UploadConstants uploadConstants = new UploadConstants();
                var productModel = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    NameAr = product.NameAr,
                    Description = product.Description,
                    DescriptionAr = product.DescriptionAr,

                };
             return productModel;
            }
            catch (Exception ex)
            {
                return new ProductModel();
            }

        }

    }
}
