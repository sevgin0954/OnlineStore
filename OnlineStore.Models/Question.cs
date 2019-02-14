using OnlineStore.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Question
    {
        public string Id { get; set; }

        [MinLength(ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        public DateTime PostDate { get; set; }

        public string CommentId { get; set; }
        public Comment Comment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
