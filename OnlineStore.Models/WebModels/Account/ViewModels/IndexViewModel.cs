using OnlineStore.Models.WebModels.DeliveryInfo.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Account.ViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public byte[] ProfileImageAsByte { get; set; }

        public List<DeliveryInfoViewModel> DeliveryInfos { get; set; }
    }
}
