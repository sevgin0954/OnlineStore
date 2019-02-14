using Microsoft.AspNetCore.Identity;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin.Interfaces
{
    public interface IAdminUsersService
    {
        Task<IList<UsersViewModel>> PrepareModelForEditingAsync();

        Task<IdentityResult> ChangeStateAsync(string userId);

        Task<IdentityResult> Delete(string userId);
    }
}
