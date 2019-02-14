using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Security.ViewModels
{
    public class IndexViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }
    }
}
