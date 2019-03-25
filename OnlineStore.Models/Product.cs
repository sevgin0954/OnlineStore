using OnlineStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Product
    {
        public string Id { get; set; }

        [MinLength(ModelsConstants.NameMinLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal? PromoPrice { get; set; }

        public int CountsLeft { get; set; }

        [MinLength(ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        public string Specifications { get; set; }

        public string SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public ICollection<OrderProduct> Orders { get; set; } = new List<OrderProduct>();

        public ICollection<UserFavoriteProduct> UserFavoriteProducts { get; set; } = new List<UserFavoriteProduct>();
    }
}
