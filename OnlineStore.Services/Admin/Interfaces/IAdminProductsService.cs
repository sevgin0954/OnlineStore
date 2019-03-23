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

        Task<IEnumerable<ProductViewModel>> GetProducts(string subcategoryId);

        Task<ProductBindingModel> PrepareModelForEditing(string productId);

        Task<bool> Edit(ProductBindingModel model, string productId);

        Task<bool> Delete(string productId);
    }
}
