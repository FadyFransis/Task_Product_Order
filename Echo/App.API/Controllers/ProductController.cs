using App.API.DTOs;
using App.API.Helper;
using App.Common.Services.Logger;
using App.Core.Interfaces.Services;
using App.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;// = new LoggerService();
      //  private readonly IOrderService _orderService;
        public ProductController(Ilogger logger, IMapper mapper,
             
        IProductService ProductService
            //, IOrderService orderService
            )
        {
            _logger = logger;
            _service = ProductService;
            _mapper = mapper;
          //  _orderService = orderService;
            
        }




        /// <summary>
        /// gfg
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [AllowAnonymous]
       
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<List<ProductBaseDTO>>> GetAll()
        {
            // if product card has rates info undo comment for rating 
            try
            {

                var result = await _service.GetAllProductsBaseModel();
                var ProducstDTO = _mapper.Map<List<ProductBaseDTO>>(result);
                //var listWithRates = _service.GetRateInfo(result.ToList());
                //     var listWithRates = result.ToList();
                //        List<ProductModel> productModels = new List<ProductModel>();
                var responseModel = HelperClass<List<ProductBaseDTO>>.CreateResponseModel(_mapper.Map<List<ProductBaseDTO>>(ProducstDTO), false, "");
                return responseModel;

            }
            catch (Exception ex)
            {
                _logger.Error("Error occured ProductController\\GetAllProductsByCategoryId" + " with EX: " + ex.ToString());
                return HelperClass<List<ProductBaseDTO>>.CreateResponseModel(null, true, ex.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
       // [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("api/[controller]/[action]")]
        public async Task<ResponseModel<ProductDetailsDTO>> GetById(long Id)
        {

            try
            {
                var result = await _service.GetProductById(Id);
                var ProductDTO = _mapper.Map<ProductDetailsDTO>(result);
                if (User.Identity.IsAuthenticated)
                {
                    string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    
                  //  ProductDTO.InMyFavouriteList = existInMyFavList != null;
                  //  ProductDTO.ProductFavoriteId = existInMyFavList != null ? existInMyFavList.Id : 0;
                   // ProductDTO.IsBoughtBefore = await _orderService.IsBoughtBefore(Id, userId);
                    //ProductDTO.IsRatedBefore = ProductDTO.ProductRates.Any() ?
                    //ProductDTO.ProductRates.Any(x => x.UserId == userId) : false;
                }
                var Product = HelperClass<ProductDetailsDTO>.CreateResponseModel(ProductDTO, false, "");
                return Product;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured ProductController\\GetById" + Id + " with EX: " + ex.ToString());
                return HelperClass<ProductDetailsDTO>.CreateResponseModel(null, true, ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseModel<ProductDetailsDTO>> AddProduct([FromBody] ProductDetailsDTO model)
        {
            try
            {
                var ProductModel = _mapper.Map<ProductModel>(model);
                var result = await _service.AddProduct(ProductModel);
                var ProductDTO = _mapper.Map<ProductDetailsDTO>(result);
                var responseModel = HelperClass<ProductDetailsDTO>.CreateResponseModel(ProductDTO, false, "");
                return responseModel;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured ProductController\\Add" + " with EX: " + ex.Message);
                return HelperClass<ProductDetailsDTO>.CreateResponseModel(null, true, ex.Message);
            }

        }


        [HttpPost]
        [Route("api/[controller]/[action]")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseModel<ProductDetailsDTO>> UpdateProduct([FromBody] ProductDetailsDTO model)
        {
            try
            {
                var ProductModel = _mapper.Map<ProductModel>(model);
                var result = await _service.UpdateProduct(ProductModel);
                var ProductDTO = _mapper.Map<ProductDetailsDTO>(result);
                var responseModel = HelperClass<ProductDetailsDTO>.CreateResponseModel(ProductDTO, false, "");
                return responseModel;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured ProductController\\UpdateProduct" + " with EX: " + ex.Message);
                return HelperClass<ProductDetailsDTO>.CreateResponseModel(null, true, ex.Message);
            }

        }

  

    }
}
