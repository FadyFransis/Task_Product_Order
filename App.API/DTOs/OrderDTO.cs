using App.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.API.DTOs
{
    public class OrderDTO : BaseDTO
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public long ShippingAddressId { get; set; }
        public long? DiscountCodeId { get; set; }
        public double DiscountAmount { get; set; }
        public double DeliveryFees { get; set; }
        public double TotalPaid { get; set; }
        public Payment PaymentMethod { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
    public class OrderLookupDTO : BaseDTO
    {
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderStatusNameAr { get; set; }
        public string OrderStatusName { get; set; }
        public UserAddressBookLookupDTO ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public long ShippingAddressId { get; set; }
        public long? DiscountCodeId { get; set; }
        public double DiscountAmount { get; set; }
        public double DeliveryFees { get; set; }
        public double TotalPaid { get; set; }
        public Payment PaymentMethod { get; set; }
        public List<OrderItemLookupDTO> OrderItems { get; set; }


    }

    public class EditOrderDTO : BaseDTO
    {
        public DateTime CreationDate { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderStatus OldStatus { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime OrderDate { get; set; }
        public long ShippingAddressId { get; set; }
        public long? DiscountCodeId { get; set; }
        public double DiscountAmount { get; set; }
        public string DiscountCodeStr { get; set; }
        public double DeliveryFees { get; set; }
        public double TotalPaid { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public Payment PaymentMethod { get; set; }
        public string CancellationReason { get; set; }


    }
}
