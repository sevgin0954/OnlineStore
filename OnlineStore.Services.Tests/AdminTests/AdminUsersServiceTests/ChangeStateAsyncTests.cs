using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineStore.Models;
using System;
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
            this.SetupUserManagerFindByIdAsyncMock(mockedUserManager, dbUser);
            this.SetupUserManagerSetLockoutEndDateAsyncMock(mockedUserManager, dbUser);

            var service = this.GetService(dbContext, mockedUserManager.Object);

            var result = await service.ChangeStateAsync(dbUserId);

            Assert.True(result.Succeeded);
        }

        private void SetupUserManagerFindByIdAsyncMock(Mock<UserManager<User>> mockedUserManager, User user)
        {
            mockedUserManager.Setup(um => um.FindByIdAsync(user.Id))
                .Returns(Task.FromResult(user));
        }

        private void SetupUserManagerSetLockoutEndDateAsyncMock(Mock<UserManager<User>> mockedUserManager, User user)
        {
            mockedUserManager.Setup(um => um.SetLockoutEndDateAsync(user, It.IsAny<DateTimeOffset>()))
                .Returns(Task.FromResult(IdentityResult.Success));
        }
    }
}
