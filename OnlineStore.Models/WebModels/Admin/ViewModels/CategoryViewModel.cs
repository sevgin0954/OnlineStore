using System.Collections.Generic;

namespace OnlineStore.Models.WebModels.Admin.ViewModels
{
    public class CategoryViewModel
    {
        public string CategoryId { get; set; }

        public string Name { get; set; }

        public long TotalProductsCount { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
