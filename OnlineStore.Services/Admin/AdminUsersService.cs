﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin
{
    public class AdminUsersService : BaseService, IAdminUsersService
    {
        public readonly IMapper mapper;
        public readonly UserManager<User> userManager;

        public AdminUsersService(
            OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UsersViewModel>> PrepareModelForEditingAsync()
        {
            var dbUsers = await GetUsersFromDatabase(false);

            var models = MapUsersViewModels(dbUsers);

            return models;
        }

        public async Task<IdentityResult> ChangeStateAsync(string userId)
        {
            var dbUser = await this.userManager
                .FindByIdAsync(userId);

            var result = new IdentityResult();

            if (dbUser == null)
            {
                result = this.CreateIdentityError(ControllerConstats.ErrorMessageWrongId);

                return result;
            }

            if (await this.userManager.IsInRoleAsync(dbUser, WebConstants.AdminRole))
            {
                result = this.CreateIdentityError(ControllerConstats.ErrorMessageCantBanYourself);

                return result;
            }

            bool isBanned = dbUser.LockoutEnd != null && dbUser.LockoutEnd > DateTime.UtcNow;

            if (isBanned)
            {
                result = await UnbanUser(dbUser);
            }
            else
            {
                result = await this.BanUser(dbUser);
            }

            return result;
        }

        public async Task<IdentityResult> Delete(string userId)
        {
            var dbUser = await this.userManager
                .FindByIdAsync(userId);

            if (dbUser == null)
            {
                var result = CreateIdentityError(ControllerConstats.ErrorMessageWrongId);

                return result;
            }

            if (await this.userManager.IsInRoleAsync(dbUser, WebConstants.AdminRole))
            {
                var result = CreateIdentityError(ControllerConstats.ErrorMessageCantDeleteYourself);

                return result;
            }

            return await this.userManager.DeleteAsync(dbUser);
        }

        private async Task<IEnumerable<User>> GetUsersFromDatabase(bool includeAdmin)
        {
            var dbUsers = this.DbContext.Users
                .Include(u => u.Orders)
                .ToList();

            if (includeAdmin == false)
            {
                await FilterAdmin(dbUsers);
            }

            return dbUsers;
        }

        private async Task FilterAdmin(IList<User> dbUsers)
        {
            foreach (var user in dbUsers)
            {
                if (await this.userManager.IsInRoleAsync(user, WebConstants.AdminRole) == true)
                {
                    dbUsers.Remove(user);

                    break;
                }
            }
        }

        private IEnumerable<UsersViewModel> MapUsersViewModels(IEnumerable<User> users)
        {
            var models = new List<UsersViewModel>();

            foreach (var user in users)
            {
                var isBanned = user.LockoutEnd > DateTime.UtcNow;

                var model = this.mapper.Map<UsersViewModel>(user);
                model.IsBanned = isBanned;

                models.Add(model);
            }

            return models;
        }

        private IdentityResult CreateIdentityError(string errorMessage)
        {
            var error = new IdentityError() { Description = errorMessage };
            var result = IdentityResult.Failed(error);

            return result;
        }

        private async Task<IdentityResult> UnbanUser(User dbUser)
        {
            var result = await this.userManager.SetLockoutEndDateAsync(dbUser, null);

            return result;
        }

        private async Task<IdentityResult> BanUser(User dbUser)
        {
            var banEndDate = new DateTimeOffset(DateTime.UtcNow + TimeSpan.FromDays(WebConstants.BanUserDays));
            var result = await this.userManager.SetLockoutEndDateAsync(dbUser, banEndDate);

            return result;
        }
    }
}
