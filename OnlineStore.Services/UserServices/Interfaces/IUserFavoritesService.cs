using OnlineStore.Models.WebModels.ProductModels.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices.Interfaces
{
    public interface IUserFavoritesService
    {
        Task<bool> RemoveProductAsync(string productId, ClaimsPrincipal user);

        Task<bool> AddProductAsync(string productId, ClaimsPrincipal user);

        Task<IEnumerable<FavoriteProductViewModel>> GetAllFavoriteProducts(ClaimsPrincipal user);
    }
}
