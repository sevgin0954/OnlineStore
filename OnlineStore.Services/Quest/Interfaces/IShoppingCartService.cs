using Microsoft.AspNetCore.Http;
using OnlineStore.Models.WebModels.Quest.BindingModels;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ProductShoppingCartViewModel>> GetProductsAsync(ISession session);

        Task AddProductAsync(string productId, ISession session);

        Task UpdateProductCountAsync(ProductCardBindingModel model, ISession session);

        Task<bool> RemoveProduct(string productId, ISession session);
    }
}
