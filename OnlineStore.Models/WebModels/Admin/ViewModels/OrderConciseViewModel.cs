namespace OnlineStore.Models.WebModels.Admin.ViewModels
{
    public class OrderConciseViewModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string OrderStatusName { get; set; }
    }
}
