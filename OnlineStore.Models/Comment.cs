using OnlineStore.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Comment
    {
        public string Id { get; set; }

        [MinLength(ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public bool IsVerified { get; set; }

        public DateTime PostDate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public string ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
