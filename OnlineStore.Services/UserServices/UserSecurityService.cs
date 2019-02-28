using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Security.BindingModels;
using OnlineStore.Models.WebModels.Security.ViewModels;
using OnlineStore.Services.UserServices.Interfaces;

namespace OnlineStore.Services.UserServices
{
    public class UserSecurityService : BaseService, IUserSecurityService
    {
        public readonly IMapper mapper;
        public readonly UserManager<User> userManager;

        public UserSecurityService(OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IndexViewModel> PrepareIndexModelForEditingAsync(ClaimsPrincipal user)
        {
            var dbUser = await this.userManager.GetUserAsync(user);

            var model = this.mapper.Map<IndexViewModel>(dbUser);

            return model;
        }

        public async Task<IdentityResult> EditEmailAsync(ClaimsPrincipal user, EmailBindingModel model)
        {
            var dbUser = await this.userManager.GetUserAsync(user);

            var result = await userManager.SetEmailAsync(dbUser, model.Email);

            return result;
        }

        public async Task<IdentityResult> EditPassword(ClaimsPrincipal user, PasswordBindingModel model)
        {
            var dbUser = await this.userManager.GetUserAsync(user);

            var result = await this.userManager.ChangePasswordAsync(dbUser, model.CurrentPassword, model.NewPassword);

            return result;
        }
    }
}
