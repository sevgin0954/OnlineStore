using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Admin.BindingModels
{
    public class CategoryBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}
