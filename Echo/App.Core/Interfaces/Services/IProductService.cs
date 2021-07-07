using App.Core.Entities;
using App.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Interfaces.Services
{
    public interface IProductService : IGenericService<Product>
    {
        
        Task<List<ProductModel>> GetAllProductsBaseModel();
        
        Task<ProductModel> AddProduct(ProductModel product);
        Task<ProductModel> UpdateProduct(ProductModel model);
        Task<ProductModel> GetProductById(long id);   
   
    }
}
