using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin.Interfaces
{
    public interface IAdminProductsService
    {
        ProductBindingModel PrepareModelForAdding(string subcategoryId);

        Task AddProduct(ProductBindingModel model);

        Task<IEnumerable<ProductViewModel>> GetProductsAsync(string subcategoryId);

        Task<ProductBindingModel> PrepareModelForEditingAsync(string productId);

        Task<bool> EditAsync(ProductBindingModel model, string productId);

        Task<bool> Delete(string productId);
    }
}
