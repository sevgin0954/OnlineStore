using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin
{
    public class AdminUsersService : BaseService, IAdminUsersService
    {
        public AdminUsersService(
            OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager) { }

        public async Task<IList<UsersViewModel>> PrepareModelForEditingAsync()
        {
            var dbUsers = this.DbContext.Users
                .Include(u => u.Orders);

            var models = new List<UsersViewModel>();

            foreach (var dbUser in dbUsers)
            {
                if (await this.UserManager.IsInRoleAsync(dbUser, WebConstants.AdminRole) == false)
                {
                    var isBanned = dbUser.LockoutEnd > DateTime.UtcNow;

                    var model = new UsersViewModel()
                    {
                        Id = dbUser.Id,
                        Email = dbUser.Email,
                        FullName = dbUser.FullName,
                        Username = dbUser.UserName,
                        OrdersCount = dbUser.Orders.Count,
                        IsBanned = isBanned
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public async Task<IdentityResult> ChangeStateAsync(string userId)
        {
            var dbUser = await this.UserManager.FindByIdAsync(userId);

            var result = new IdentityResult();

            if (await this.UserManager.IsInRoleAsync(dbUser, WebConstants.AdminRole))
            {
                var error = new IdentityError() { Description = ControllerConstats.ErrorMessageCantBanYourself };
                result = IdentityResult.Failed(error);

                return result;
            }

            bool isBanned = dbUser.LockoutEnd != null && dbUser.LockoutEnd > DateTime.UtcNow;

            if (isBanned)
            {
                result = await this.UserManager.SetLockoutEndDateAsync(dbUser, null);
            }
            else
            {
                var banDate = new DateTimeOffset(DateTime.UtcNow + TimeSpan.FromDays(WebConstants.BanUserDays));
                result = await this.UserManager.SetLockoutEndDateAsync(dbUser, banDate);
            }

            return result;
        }

        public async Task<IdentityResult> Delete(string userId)
        {
            var dbUser = await this.UserManager.FindByIdAsync(userId);

            var result = new IdentityResult();

            if (await this.UserManager.IsInRoleAsync(dbUser, WebConstants.AdminRole))
            {
                var error = new IdentityError() { Description = ControllerConstats.ErrorMessageCantDeleteYourself };
                result = IdentityResult.Failed(error);

                return result;
            }

            return await this.UserManager.DeleteAsync(dbUser);
        }
    }
}
