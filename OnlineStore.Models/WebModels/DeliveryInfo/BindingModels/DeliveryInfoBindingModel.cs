using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.DeliveryInfo.BindingModels
{
    public class DeliveryInfoBindingModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [MinLength(6)]
        [MaxLength(20)]
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [MinLength(5)]
        [MaxLength(200)]
        [Required]
        public string Address { get; set; }

        [Display(Name = "District")]
        //public string SelectedDistrictName { get; set; }
        public ICollection<SelectListItem> AllDistricts { get; set; }

        [Required]
        public string SelectedDistrictId { get; set; }

        [Display(Name = "Populated Place")]
        //public string SelectedPopulatedPlaceName { get; set; }
        public ICollection<SelectListItem> AllPopulatedPlaces { get; set; }

        [Required]
        public string SelectedPopulatedPlaceId { get; set; }
    }
}
