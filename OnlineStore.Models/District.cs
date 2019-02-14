using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class District
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<PopulatedPlace> PopulatedPlaces { get; set; } = new List<PopulatedPlace>();

        public ICollection<DeliveryInfo> DeliverysInfos { get; set; } = new List<DeliveryInfo>();
    }
}
