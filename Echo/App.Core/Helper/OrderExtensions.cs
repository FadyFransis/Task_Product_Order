using App.Core.Constants;
using App.Core.Entities;
using App.Core.Entities.Base;
using App.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace App.Core.Helper
{
    public static class OrderExtensions
    {

        public static IQueryable<OrderModel> GetOrdersModel(this IQueryable<Order> orders)
        {
            
            var ordersModel = orders.Select(x => new OrderModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                OrderNumber = x.OrderNumber,
                OrderStatus = x.OrderStatus,
                OrderStatusName = x.OrderStatus.ToString(),
                OrderStatusNameAr= GetOrderStatusNameArabic(x.OrderStatus),
                OrderDate = x.OrderDate,
                UserId = x.UserId,
                TotalPaid = x.TotalPaid,
                UserFullName = x.User.FirstName + " " + x.User.LastName,
                UserEmail = x.User.Email,
                OrderItems = x.OrderItems.GetOrderItemsModel(),
            }).OrderByDescending(x=>x.Id);
            return ordersModel;

        }

        public static OrderModel GetOrderModel(this Order order)
        {
            OrderModel orderModel = new OrderModel()
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                OrderStatusName = order.OrderStatus.ToString(),
                OrderStatusNameAr = GetOrderStatusNameArabic(order.OrderStatus),
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                OrderItems = order.OrderItems.GetOrderItemsModel(),
            };

            return orderModel;

        }


        public static OrderModel GetBaseOrderModel(this Order order)
        {
            OrderModel orderModel = new OrderModel()
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                OrderStatusName = order.OrderStatus.ToString(),
                OrderStatusNameAr = GetOrderStatusNameArabic(order.OrderStatus),
                OrderDate = order.OrderDate,
                UserId = order.UserId,
            };

            return orderModel;

        }
        public static IQueryable<OrderModel> GetOrdersBaseModel(this IQueryable<Order> orders)
        {

            var ordersModel = orders.Select(x => new OrderModel
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                OrderStatus = x.OrderStatus,
                OrderStatusName = x.OrderStatus.ToString(),
                OrderStatusNameAr = GetOrderStatusNameArabic(x.OrderStatus),
                OrderDate = x.OrderDate,
                UserId = x.UserId,
            });
            return ordersModel;

        }
        public static List<OrderItemModel> GetOrderItemsModel(this ICollection<OrderItem> orderItems)
        {
            UploadConstants uploadConstants = new UploadConstants();
            var orderItemsModel = orderItems.Select(i => new OrderItemModel
            {

                Id = i.Id,
                ProductId = i.Product.Id,
                ProductName = i.Product.Name,
                ProductNameAr = i.Product.NameAr,
                ProductSizeName = i.Product.Name,
                ProductSizeNameAr = i.Product.NameAr,
                Price = i.Price,
                Quantity = i.Quantity,
                OrderId = i.OrderId,
           
            });
            return orderItemsModel.ToList();
        }

        public static string GetOrderStatusNameArabic(OrderStatus orderStatus)
        {
            string orderStatusArabic = string.Empty;
            switch (orderStatus)
            {

                case OrderStatus.Submitted:
                    orderStatusArabic = "تم قبول الطلب";
                    break;

                case OrderStatus.Confirmed:
                    orderStatusArabic = "تم التاكيد الطلب";
                    break;

                case OrderStatus.Ontrack:
                    orderStatusArabic = "فى طريقه اليك";
                    break;
                case OrderStatus.Completed:
                    orderStatusArabic = "تم التوصيل ";
                    break;

                default:
                    orderStatusArabic = "تم الالغاء";
                    break;

            }

            return orderStatusArabic;

        }

    }
}
