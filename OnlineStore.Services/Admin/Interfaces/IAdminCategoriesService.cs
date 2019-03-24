using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin.Interfaces
{
    public interface IAdminCategoriesService
    {
        IList<CategoryViewModel> GetAllCategories();

        Task CreateCategoryAsync(CategoryBindingModel model);

        Task<SubCategoryBindingCategory> PrepareSubCategoryModelForAdding(string categoryId);

        Task AddSubcategory(SubCategoryBindingCategory model);
    }
}
