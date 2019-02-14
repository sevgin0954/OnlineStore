using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class OrderStatus
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
