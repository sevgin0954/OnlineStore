using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Account.BindingModels
{
    public class PersonInfoBindingModel
    {
        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
