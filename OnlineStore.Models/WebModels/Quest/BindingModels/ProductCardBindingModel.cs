using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Quest.BindingModels
{
    public class ProductCardBindingModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int OrderQuantity { get; set; }
    }
}
