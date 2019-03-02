using OnlineStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class DeliveryInfo
    {
        public string Id { get; set; }

        [MinLength(ModelsConstants.AddressMinLength)]
        public string Address { get; set; }

        [MinLength(ModelsConstants.NameMinLength)]
        public string FullName { get; set; }

        [MinLength(ModelsConstants.PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        public string DistrictId { get; set; }
        public District District { get; set; }

        public string PopulatedPlaceId { get; set; }
        public PopulatedPlace PopulatedPlace { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
