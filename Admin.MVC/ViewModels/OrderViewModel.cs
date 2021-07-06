using App.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace Admin.MVC.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public DateTime CreationDate { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderStatus OldStatus { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime OrderDate { get; set; }
        public long ShippingAddressId { get; set; }
        public UserAddressBookViewModel ShippingAddress { get; set; }
        public long? DiscountCodeId { get; set; }
        public double DiscountAmount { get; set; }
        public string DiscountCodeStr { get; set; }
        public double DeliveryFees { get; set; }
        public double TotalPaid { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public Payment PaymentMethod { get; set; }
        public string CancellationReason { get; set; }
    }
}
