using Microsoft.AspNetCore.Http;
using OnlineStore.Models.WebModels.Account.BindingModels;
using OnlineStore.Models.WebModels.Account.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices.Interfaces
{
    public interface IUserProfileService
    {
        Task<IndexViewModel> PrepareIndexForEditingAsync(ClaimsPrincipal user);

        Task UpdateProfilePictureAsync(ClaimsPrincipal user, IFormFile picture);

        Task<PersonInfoBindingModel> PreparePersonInfoForEditingAsync(ClaimsPrincipal user);

        Task EditPersonInfoAsync(ClaimsPrincipal user, PersonInfoBindingModel model);
    }
}
