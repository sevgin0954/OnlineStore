using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.OrderModels.BindingModels
{
    public class ProductConciseBindingModel
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Photo MainPhoto { get; set; }
    }
}
