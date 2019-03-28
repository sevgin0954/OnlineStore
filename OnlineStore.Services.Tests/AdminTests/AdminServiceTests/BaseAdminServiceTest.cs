using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;

namespace OnlineStore.Services.Tests.AdminTests.AdminServiceTests
{
    public abstract class BaseAdminServiceTest : BaseTest
    {
        public IAdminService GetService(OnlineStoreDbContext dbContext)
        {
            var userManger = this.GetUserManager(dbContext);
            var service = new AdminService(dbContext, userManger);

            return service;
        }

        private UserManager<User> GetUserManager(OnlineStoreDbContext dbContext)
        {
            var userStore = new Mock<IUserStore<User>>().Object;
            var mock = new Mock<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);

            var dbUsers = dbContext.Users;

            mock.Setup(um => um.Users).Returns(dbUsers);

            return mock.Object;
        }
    }
}