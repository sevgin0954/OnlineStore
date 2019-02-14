using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class SubCategory
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
