using OnlineStore.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Security.BindingModels
{
    public class PasswordBindingModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = ControllerConstats.ErrorMessagePasswordDontMatch)]
        public string ConfirmPassword { get; set; }
    }
}
