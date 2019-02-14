using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public string PhotoId { get; set; }
        public Photo ProfilePicture { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<DeliveryInfo> DeliveryInfos { get; set; } = new List<DeliveryInfo>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
