using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.OrderModels.BindingModels
{
    public class PaymentTypeBindingModel
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
