using App.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPaid { get; set; }
        public AppUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
