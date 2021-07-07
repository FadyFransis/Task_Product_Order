using App.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Models
{
    public class OrderModel : BaseModel
    {
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderStatus OldStatus { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderStatusNameAr { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public long ShippingAddressId { get; set; }
        public long? DiscountCodeId { get; set; }
        public string DiscountCodeStr { get; set; }
        public double DiscountAmount { get; set; }
        public double DeliveryFees { get; set; }
        public double TotalPaid { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public Payment PaymentMethod { get; set; }
        public string CancellationReason { get; set; }

    }
}
