using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Security.BindingModels
{
    public class EmailBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
