using System.Collections.Generic;

namespace OnlineStore.Models.WebModels.ProductModels.ViewModels
{
    public class FavoriteProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] MainPhoto { get; set; }
    }
}
