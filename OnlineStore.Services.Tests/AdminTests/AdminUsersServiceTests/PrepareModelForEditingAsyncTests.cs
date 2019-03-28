using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminUsersServiceTests
{
    public class PrepareModelForEditingAsyncTests : BaseAdminUsersServiceTest
    {
        [Fact]
        public async Task WithoutUser_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = this.GetMockedUserManager();

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var models = await service.PrepareModelForEditingAsync();
            var modelsCount = models.Count();

            Assert.Equal(0, modelsCount);
        }

        [Fact]
        public async Task WithtAdmin_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var dbUserAdmin = new User();
            var dbRole = new IdentityRole()
            {
                Name = WebConstants.AdminRole
            };
            this.AddRoleToUser(dbContext, dbUserAdmin, dbRole);

            var mockedUserManager = this.GetMockedUserManager();
            mockedUserManager.Setup(um => um.IsInRoleAsync(dbUserAdmin, WebConstants.AdminRole))
                .Returns(Task.FromResult(true));

            var service = this.GetService(dbContext, mockedUserManager.Object);
            var models = await service.PrepareModelForEditingAsync();
            var modelsCount = models.Count();

            Assert.Equal(0, modelsCount);
        }

        [Fact]
        public async Task WithTwoUsers_ShouldReturnCorrectModelsCount()
        {
            var dbUser1 = new User();
            var dbUser2 = new User();
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser1, dbUser2);
            var modelsCount = models.Count();

            Assert.Equal(2, modelsCount);
        }

        [Fact]
        public async Task WithUserWithId_ShouldReturnModelWithCorrectId()
        {
            var dbUser = new User();
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelId = firstModel.Id;
            var dbUserId = dbUser.Id;

            Assert.Equal(dbUserId, firstModelId);
        }

        [Theory]
        [InlineData("Username")]
        public async Task WithUserWithUsername_ShouldReturnModelWithCorrectUsername(string username)
        {
            var dbUser = new User()
            {
                UserName = username
            };
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelUsername = firstModel.Username;

            Assert.Equal(username, firstModelUsername);
        }

        [Theory]
        [InlineData("FullName")]
        public async Task WithUserWithFullName_ShouldReturnModelWithCorrectFullName(string fullName)
        {
            var dbUser = new User()
            {
                FullName = fullName
            };
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelFullName = firstModel.FullName;

            Assert.Equal(fullName, firstModelFullName);
        }

        [Theory]
        [InlineData("Email")]
        public async Task WithUserWithEmail_ShouldReturnModelWithCorrectEmail(string email)
        {
            var dbUser = new User()
            {
                Email = email
            };
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelEmail = firstModel.Email;

            Assert.Equal(email, firstModelEmail);
        }

        [Fact]
        public async Task WithUserWithOneOrder_ShouldReturnModelWithCorrectOrdersCount()
        {
            var dbOrder = new Order();
            var dbUser = new User();
            dbUser.Orders.Add(dbOrder);
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var ordersCount = firstModel.OrdersCount;

            Assert.Equal(1, ordersCount);
        }

        [Fact]
        public async Task WithUserWithNotExpiredLockoutEnd_ShouldReturnModelWithTrueIsBannedFlag()
        {
            var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow.AddDays(1));
            var dbUser = new User()
            {
                LockoutEnd = dateTimeOffset
            };
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelIsBannedFlag = firstModel.IsBanned;

            Assert.True(firstModelIsBannedFlag);
        }

        [Fact]
        public async Task WithUserWithExpiredLockoutEnd_ShouldReturnModelWithFalseIsBannedFlag()
        {
            var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
            var dbUser = new User()
            {
                LockoutEnd = dateTimeOffset
            };
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelIsBannedFlag = firstModel.IsBanned;

            Assert.False(firstModelIsBannedFlag);
        }

        [Fact]
        public async Task WithUserWithoutLockoutEnd_ShouldReturnModelWithFalseIsBannedFlag()
        {
            var dbUser = new User();
            var mockedUserManager = this.GetMockedUserManager();

            var models = await this.CallPrepareModelForEditingAsyncWithUsers(mockedUserManager.Object, dbUser);
            var firstModel = models.First();
            var firstModelIsBannedFlag = firstModel.IsBanned;

            Assert.False(firstModelIsBannedFlag);
        }

        private void AddRoleToUser(OnlineStoreDbContext dbContext, User user, IdentityRole role)
        {
            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.SaveChanges();

            var dbUserRole = new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            dbContext.UserRoles.Add(dbUserRole);

            dbContext.SaveChanges();
        }

        private async Task<IEnumerable<UserViewModel>> CallPrepareModelForEditingAsyncWithUsers(
            UserManager<User> userManager,
            params User[] users)
        {
            var dbContext = this.GetDbContext();
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
            
            var service = this.GetService(dbContext, userManager);
            var models = await service.PrepareModelForEditingAsync();

            return models;
        }
    }
}
