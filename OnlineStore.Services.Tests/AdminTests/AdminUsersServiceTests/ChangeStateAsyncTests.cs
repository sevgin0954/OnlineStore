using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineStore.Common.Constants;
using OnlineStore.Models;
using OnlineStore.Services.Tests.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminUsersServiceTests
{
    public class ChangeStateAsyncTests : BaseAdminUsersServiceTest
    {
        [Fact]
        public async Task WithIncorrectUserId_ShouldReturnIdentityResultWithFalseSucceededFlag()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = this.GetMockedUserManager();
            var service = this.GetService(dbContext, mockedUserManager.Object);

            var userId = Guid.NewGuid().ToString();
            var result = await service.ChangeStateAsync(userId);

            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task WithCorrectUserId_ShouldReturnIdentityResultWithTrueSucceededFlag()
        {
            var dbContext = this.GetDbContext();
            var dbUser = new User();
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            var dbUserId = dbUser.Id;

            var mockedUserManager = this.GetMockedUserManager();
            CommonTestMethods.SetupUserManagerFindByIdAsyncMock(mockedUserManager, dbUser);
            CommonTestMethods.SetupUserManagerSetLockoutEndDateAsyncWithEndDateMock(mockedUserManager, dbUser);

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var result = await service.ChangeStateAsync(dbUserId);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task WithAdminUserId_ShouldReturnIdentityResultWithCorrectDescription()
        {
            var dbContext = this.GetDbContext();
            var dbUser = new User();
            var dbRole = new IdentityRole()
            {
                Name = WebConstants.AdminRoleName
            };
            CommonTestMethods.AddRoleToUser(dbContext, dbUser, dbRole);

            var mockedUserManager = this.GetMockedUserManager();
            CommonTestMethods.SetupUserManagerFindByIdAsyncMock(mockedUserManager, dbUser);
            CommonTestMethods.SetupUserManagerIsInRoleAsyncMock(mockedUserManager, dbUser, true);

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var result = await service.ChangeStateAsync(dbUser.Id);
            var resultFirstError = result.Errors.First();
            var resultFirstErrorMessage = resultFirstError.Description;

            Assert.Equal(ControllerConstats.ErrorMessageCantBanYourself, resultFirstErrorMessage);
        }

        [Fact]
        public async Task WithBannedUserWithId_ShouldCallUnbanUser()
        {
            var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow.AddDays(1));

            var dbContext = this.GetDbContext();
            var dbUser = new User()
            {
                LockoutEnd = dateTimeOffset
            };
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            var mockedUserManager = this.GetMockedUserManager();
            CommonTestMethods.SetupUserManagerFindByIdAsyncMock(mockedUserManager, dbUser);
            CommonTestMethods.SetupUserManagerIsInRoleAsyncMock(mockedUserManager, dbUser, false);
            CommonTestMethods.SetupUserManagerSetLockoutEndDateAsyncWithNullEndDateMock(mockedUserManager, dbUser);

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var result = await service.ChangeStateAsync(dbUser.Id);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task WithUnbannedUserWithId_ShouldCallBanUser()
        {
            var dbContext = this.GetDbContext();
            var dbUser = new User();
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            var mockedUserManager = this.GetMockedUserManager();
            CommonTestMethods.SetupUserManagerFindByIdAsyncMock(mockedUserManager, dbUser);
            CommonTestMethods.SetupUserManagerIsInRoleAsyncMock(mockedUserManager, dbUser, false);
            CommonTestMethods.SetupUserManagerSetLockoutEndDateAsyncWithEndDateMock(mockedUserManager, dbUser);

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var result = await service.ChangeStateAsync(dbUser.Id);

            Assert.True(result.Succeeded);
        }
    }
}
