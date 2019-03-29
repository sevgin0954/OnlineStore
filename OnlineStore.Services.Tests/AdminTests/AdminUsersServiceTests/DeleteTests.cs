using Microsoft.AspNetCore.Identity;
using OnlineStore.Common.Constants;
using OnlineStore.Models;
using OnlineStore.Services.Tests.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminUsersServiceTests
{
    public class DeleteTests : BaseAdminUsersServiceTest
    {
        [Fact]
        public async Task WithIncorrectUserId_ShouldReturnIdentityResultWithCorrectStatusDescription()
        {
            var dbContext = this.GetDbContext();
            var mockedUserManager = this.GetMockedUserManager();
            var service = this.GetService(dbContext, mockedUserManager.Object);

            var incorrectDbUserId = Guid.NewGuid().ToString();

            var result = await service.Delete(incorrectDbUserId);
            var firstResultError = result.Errors.First();
            var firstResultErrorDescription = firstResultError.Description;
            var expectetErrorDescription = ControllerConstats.ErrorMessageWrongId;

            Assert.Equal(expectetErrorDescription, firstResultErrorDescription);
        }

        [Fact]
        public async Task WithAdminUserId_ShouldReturnIdentityResultWithCorrectStatusDescription()
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

            var dbUserId = dbUser.Id;

            var result = await service.Delete(dbUserId);
            var firstResultError = result.Errors.First();
            var firstResultErrorDescription = firstResultError.Description;
            var expectetErrorDescription = ControllerConstats.ErrorMessageCantDeleteAdmin;

            Assert.Equal(expectetErrorDescription, firstResultErrorDescription);
        }

        [Fact]
        public async Task WithCorrectUserId_ShouldReturnIdentityResultWithTrueSucceededFlaf()
        {
            var dbContext = this.GetDbContext();
            var dbUser = new User();
            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            var mockedUserManager = this.GetMockedUserManager();
            CommonTestMethods.SetupUserManagerFindByIdAsyncMock(mockedUserManager, dbUser);
            CommonTestMethods.SetupUserManagerIsInRoleAsyncMock(mockedUserManager, dbUser, false);
            CommonTestMethods.SetupUserManagerDeleteAsyncMock(mockedUserManager, dbUser);

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var dbUserId = dbUser.Id;
            var result = await service.Delete(dbUserId);

            Assert.True(result.Succeeded);
        }
    }
}
