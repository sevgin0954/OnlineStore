using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;
using OnlineStore.Services.Tests.Common;

namespace OnlineStore.Services.Tests.AdminTests.AdminUsersServiceTests
{
    public abstract class BaseAdminUsersServiceTest : BaseTest
    {
        private readonly IMapper mapper;

        public BaseAdminUsersServiceTest()
        {
            this.mapper = CommonTestMethods.GetAutoMapper();
        }

        public IAdminUsersService GetService(OnlineStoreDbContext dbContext, UserManager<User> userManager)
        {
            var service = new AdminUsersService(dbContext, mapper, userManager);
            return service;
        }

        public Mock<UserManager<User>> GetMockedUserManager()
        {
            var userStore = new Mock<IUserStore<User>>().Object;
            var mockedUserManager = new Mock<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);

            return mockedUserManager;
        }
    }
}
