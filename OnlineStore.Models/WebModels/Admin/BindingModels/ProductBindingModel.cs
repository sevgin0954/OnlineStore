using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Admin.BindingModels
{
    public class ProductBindingModel
    {
        public string ProductId { get; set; }

        [Required]
        public string SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? PromoPrice { get; set; }

        [Required]
        public int CountsLeft { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Specifications { get; set; }

        public ICollection<IFormFile> Photos { get; set; } = new List<IFormFile>();
    }
}
