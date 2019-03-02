using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.OrderModels.BindingModels
{
    public class DeliveryInfoBindingModel
    {
        [Required]
        public string Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string SelectedDistrictName { get; set; }

        public string SelectedPopulatedName { get; set; }
    }
}
