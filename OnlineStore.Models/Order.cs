using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DeliveryExpectedTime { get; set; }

        public decimal DeliveryPrice { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public string PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public string DeliveryInfoId { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }

        public string OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
