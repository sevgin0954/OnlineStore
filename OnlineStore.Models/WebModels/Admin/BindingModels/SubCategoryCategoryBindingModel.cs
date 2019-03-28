using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.WebModels.Admin.BindingModels
{
    public class SubCategoryCategoryBindingModel
    {
        [Required]
        public string CategoryId { get; set; }

        [BindNever]
        public string CategoryName { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}
