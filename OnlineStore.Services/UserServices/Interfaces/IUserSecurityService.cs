using Microsoft.AspNetCore.Identity;
using OnlineStore.Models.WebModels.Security.BindingModels;
using OnlineStore.Models.WebModels.Security.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices.Interfaces
{
    public interface IUserSecurityService
    {
        Task<IndexViewModel> PrepareIndexModelForEditingAsync(ClaimsPrincipal user);

        Task<IdentityResult> EditEmailAsync(ClaimsPrincipal user, EmailBindingModel model);

        Task<IdentityResult> EditPassword(ClaimsPrincipal user, PasswordBindingModel model);
    }
}
