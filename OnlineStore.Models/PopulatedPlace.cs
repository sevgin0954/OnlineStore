using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class PopulatedPlace
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DistrictId { get; set; }
        public District District { get; set; }

        public ICollection<DeliveryInfo> DeliveryInfos { get; set; } = new List<DeliveryInfo>();
    }
}
