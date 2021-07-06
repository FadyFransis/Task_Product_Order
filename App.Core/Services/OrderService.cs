using App.Common.Models;
using App.Common.Services.Logger;
using App.Common.Services.Mail;
using App.Core.Entities;
using App.Core.Entities.Base;
using App.Core.Helper;
using App.Core.Interfaces.Repository;
using App.Core.Interfaces.Services;
using App.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class OrderService : GenericService<Order>, IOrderService
    {
   
        private readonly IMailNotification _mailnotification;
        public OrderService(IMailNotification mailnotification,
            IGenericRepository<Order> oRepository, 
            Ilogger logger,
            IMapper mapper)
            : base(oRepository, logger, mapper)
        {
            
            _mailnotification = mailnotification;
        }
        public async Task<IQueryable<OrderModel>> GetUserOrders(string userId)
        {
            var orders = await GetAllIncludeString<Order>("id", x => x.UserId == userId, null, null, new string[]
                { "OrderItems","OrderItems.ProductSize","OrderItems.ProductSize.Product","ShippingAddress","ShippingAddress.Area",
                "OrderItems.ProductSize.Product.ProductImages"});
            return orders.Result.GetOrdersModel();
        }

        public async Task<IQueryable<OrderModel>> GetAllOrdersBaseModels()
        {
            var orders = await GetAllIncludeString<Order>("id", null, null, null, new string[]
                { "ShippingAddress","ShippingAddress.Area","User","OrderItems"});
            return orders.Result.GetOrdersBaseModel();
        }

        public async Task<IQueryable<OrderModel>> GetAllOrders()
        {
            var orders = await GetAllIncludeString<Order>("id", null, null, null, new string[]
                { "OrderItems","OrderItems.ProductSize","OrderItems.ProductSize.Product","ShippingAddress","ShippingAddress.Area"});
            return orders.Result.GetOrdersModel();
        }

        public async Task<OrderModel> GetOrderById(long orderId)
        {
            var order = await GetByIdIncludeString<Order>(orderId, new string[]
                { "OrderItems","OrderItems.ProductSize","OrderItems.ProductSize.Product",
                    "OrderItems.ProductSize.Product.ProductImages",
                    "ShippingAddress","ShippingAddress.Area","User","DiscountCode"});
            return order.GetOrderModel();
        }

        public async Task<OrderModel> GetBaseOrderById(long orderId)
        {
            var order = await GetByIdIncludeString<Order>(orderId, new string[]
                { "User"});
            return order.GetBaseOrderModel();
        }
        public async Task<OrderModel> AddOrder(OrderModel model)
        {
            model.OrderStatus = Entities.Base.OrderStatus.Submitted;
            model.OrderNumber = DateTime.Now.Ticks.ToString();
            var newOrder = mapper.Map<Order>(model);
            newOrder.User = null;
            var result = await Add(newOrder);
            var orderModel = await GetOrderById(result.Id);
            
            return orderModel;
        }

        public async Task<OrderModel> EditOrder(OrderModel model)
        {
            var order = mapper.Map<Order>(model);
            var result = await Update(order.Id, order);
            var orderModel = await GetBaseOrderById(result.Id);

            // check on change status
          
            return orderModel;
        }

    
        public async Task<BooleanDescriptionResultDTO> CancelOrder(long orderId)
        {
            
            var _baseOrder =await GetById<Order>(orderId,x=>x.User);
            if(_baseOrder.OrderStatus == OrderStatus.Submitted)
            {
                _baseOrder.OrderStatus = OrderStatus.Canceled;
                
                var result =  await Update(orderId, _baseOrder);
                // check on change status
               
            }
            else
                return new BooleanDescriptionResultDTO() { Success = false, Description = "Cant Cancel " + _baseOrder.OrderStatus.ToString() +" Order" };
                    
            return new BooleanDescriptionResultDTO() { Success = true, Description = "Order Cancelled" };
        }
    }
}
