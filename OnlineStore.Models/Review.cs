using OnlineStore.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Review
    {
        public string Id { get; set; }

        [MinLength(ModelsConstants.TitleMinLength)]
        public string Title { get; set; }

        [MinLength(ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [MinLength(1)]
        public int StarsCount { get; set; }

        public bool IsVerified { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public DateTime PostDate { get; set; }

        public ICollection<Photo> Pictures { get; set; } = new List<Photo>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
