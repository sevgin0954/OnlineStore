using OnlineStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class PaymentType
    {
        public string Id { get; set; }

        [MinLength(ModelsConstants.NameMinLength)]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
