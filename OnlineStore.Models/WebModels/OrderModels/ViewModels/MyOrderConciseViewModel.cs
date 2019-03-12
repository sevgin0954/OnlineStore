using System;

namespace OnlineStore.Models.WebModels.OrderModels.ViewModels
{
    public class MyOrderConciseViewModel
    {
        public string Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
        
        public string OrderStatusName { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
