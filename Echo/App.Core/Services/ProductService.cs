using AutoMapper;
using App.Common.Services.Logger;
using App.Core.Entities;
using App.Core.Interfaces.Repository;
using App.Core.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Models;
using App.Core.Helper;
using LinqKit;
using System.Collections.Generic;

namespace App.Core.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IGenericRepository<OrderItem> _orderItemsRepository;

        public ProductService(
            IGenericRepository<Product> oRepository,

        Ilogger logger, IGenericRepository<OrderItem> orderItemsRepository,
            IMapper mapper)
            : base(oRepository, logger, mapper)
        {
             _orderItemsRepository = orderItemsRepository;
  

        }
     

   



        public async Task<List<ProductModel>> GetLatestProducts()
        {
            var products = await GetAllIncludeString<Product>("-id", null, 0, 6, new string[]
              { "Brand","ProductImages","ProductFavorites","Category","ProductSizes"});
           
            return products.Result.GetProductsBaseModels().ToList();
        }
    
       
        public async Task<ProductModel> GetProductById(long id)
        {
            var product = await GetByIdIncludeString<Product>(id, new string[]
                { "Brand","ProductImages","ProductFavorites",
                    "Category","ProductRates","ProductRates.User","ProductSizes"});
            return  product.GetProductModel();

        }

    

       

       


        public async Task<ProductModel> AddProduct(ProductModel product)
        {
            var newproduct = mapper.Map<Product>(product);
            var result = await Add(newproduct);
            var productModel = mapper.Map<ProductModel>(result);
            return productModel;
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var product = mapper.Map<Product>(model);
            var result = await Update(product.Id, product);
            var productModel = mapper.Map<ProductModel>(result);
            return productModel;
        }

        public async Task<List<ProductModel>> GetAllProductsBaseModel()
        {
            var products = await GetAllIncludeString<Product>("id", null, null, null, new string[]
                { });
            var productModels = products.Result.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                NameAr = x.NameAr,
                CreationDate = x.CreationDate,
                Description = x.Description,
                DescriptionAr = x.DescriptionAr,
                Price =x.Price,
                Stock = x.Stock,
            

            }).ToList();
          
            return productModels.ToList();
        }


        //public async Task<List<ProductModel>> UpdateRealStock(IQueryable<ProductModel> productModels)
        //{
        //    List<ProductModel> models = new List<ProductModel>();

        //    foreach (var item in productModels)
        //    {
        //        ProductModel model = item;
        //        int outOfStock = await GetSelledProductQuantityByProductId(item.Id);
              
        //        model.Stock = item.Stock - outOfStock;
        //        models.Add(item);
        //    }
        //    return models;

        //}

        //public async Task<List<ProductModel>> UpdateRealStock(List<ProductModel> productModels)
        //{
        //    List<ProductModel> models = new List<ProductModel>();

        //    foreach (var item in productModels)
        //    {
        //        ProductModel model = item;
        //        int outOfStock = await GetSelledProductQuantityByProductId(item.Id);

        //        model.Stock = item.Stock - outOfStock;
        //        models.Add(item);
        //    }
        //    return models;

        //}



        //public async Task<ProductModel> UpdateRealStock(ProductModel productModel)
        //{

        //    ProductModel model = productModel;
        //  int outOfStock = await GetSelledProductQuantityByProductId(productModel.Id);
          
        //    model.Stock = productModel.Stock - outOfStock;
        //    return model;
        //}

        
      
     
    }
}
