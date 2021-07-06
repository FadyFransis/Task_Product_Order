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
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;// = new LoggerService();

        public OrderController(Ilogger logger, IMapper mapper, IOrderService OrderService)
        {
            _logger = logger;
            _service = OrderService;
            _mapper = mapper;
          
        }

        /// <summary>
        /// Authorized API 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseModel<List<OrderLookupDTO>>> GetUserOrders()
        {

            try
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _service.GetUserOrders(userId);
                var list = _mapper.Map<List<OrderLookupDTO>>(result.ToList());
                var orders = HelperClass<List<OrderLookupDTO>>.CreateResponseModel(list, false, "");
                return orders;

            }
            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\GetAll" + " with EX: " + ex.ToString());
                return HelperClass<List<OrderLookupDTO>>.CreateResponseModel(null, true, ex.Message);
            }
        }

        /// <summary>
        /// Authorized API 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ResponseModel<OrderDTO>> AddOrder([FromBody] OrderDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return HelperClass<OrderDTO>.CreateResponseModel(null, true,
                      string.Join(",", ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)));

                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var OrderModel = _mapper.Map<OrderModel>(model);
                OrderModel.UserId = userId;
                var result = await _service.AddOrder(OrderModel);
                var OrderDTO = _mapper.Map<OrderDTO>(result);
                var order = HelperClass<OrderDTO>.CreateResponseModel(OrderDTO, false, "");
                
                return order;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\Add" + " with EX: " + ex.Message);
                return HelperClass<OrderDTO>.CreateResponseModel(null, true, ex.Message);
            }

        }


        [HttpPost]
        [Route("api/[controller]/[action]")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseModel<EditOrderDTO>> UpdateOrder([FromBody] EditOrderDTO model)
        {
            try
            {
                var OrderModel = _mapper.Map<OrderModel>(model);
                var result = await _service.EditOrder(OrderModel);
                var OrderDTO = _mapper.Map<EditOrderDTO>(result);
                var order = HelperClass<EditOrderDTO>.CreateResponseModel(OrderDTO, false, "");
                return order;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\UpdateOrder" + " with EX: " + ex.Message);
                return HelperClass<EditOrderDTO>.CreateResponseModel(null, true, ex.Message);
            }

        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<ResponseModel<BooleanDescriptionResultDTO>> CancelOrder([FromBody] BaseDTO model)
        {
            try
            {
               
                var result = await _service.CancelOrder(model.Id);
                var order = HelperClass<BooleanDescriptionResultDTO>.CreateResponseModel(result, false, "");
                return order;
            }

            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\CancelOrder" + " with EX: " + ex.Message);
                return HelperClass<BooleanDescriptionResultDTO>.CreateResponseModel(null, true, ex.Message);
            }

        }

    }
}
