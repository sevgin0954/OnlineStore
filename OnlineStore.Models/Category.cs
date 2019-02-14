using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
