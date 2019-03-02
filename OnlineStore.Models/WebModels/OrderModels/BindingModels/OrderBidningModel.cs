using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.OrderModels.BindingModels
{
    public class OrderBidningModel
    {
        [Required]
        public IList<ProductConciseBindingModel> Products { get; set; } = new List<ProductConciseBindingModel>();

        [Required]
        public DeliveryInfoBindingModel DeliveryInfo { get; set; }

        [Required]
        public string SelectedPaymentTypeId { get; set; }

        public IList<SelectListItem> PaymentTypes { get; set; } = new List<SelectListItem>();
    }
}
