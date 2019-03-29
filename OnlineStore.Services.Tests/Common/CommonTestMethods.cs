using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Web.Mapping;
using System;
using System.Threading.Tasks;

namespace OnlineStore.Services.Tests.Common
{
    internal static class CommonTestMethods
    {
        internal static IMapper GetAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();

            return mapper;
        }

        internal static void AddRoleToUser(OnlineStoreDbContext dbContext, User user, IdentityRole role)
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

        internal static void SetupUserManagerFindByIdAsyncMock(Mock<UserManager<User>> mockedUserManager, User user)
        {
            mockedUserManager.Setup(um => um.FindByIdAsync(user.Id))
                .Returns(Task.FromResult(user));
        }

        internal static void SetupUserManagerSetLockoutEndDateAsyncWithEndDateMock(
            Mock<UserManager<User>> mockedUserManager, User user)
        {
            mockedUserManager.Setup(um => um.SetLockoutEndDateAsync(user, It.IsAny<DateTimeOffset>()))
                .Returns(Task.FromResult(IdentityResult.Success));
        }

        internal static void SetupUserManagerSetLockoutEndDateAsyncWithNullEndDateMock(
            Mock<UserManager<User>> mockedUserManager, User user)
        {
            mockedUserManager.Setup(um => um.SetLockoutEndDateAsync(user, null))
                .Returns(Task.FromResult(IdentityResult.Success));
        }

        internal static void SetupUserManagerIsInRoleAsyncMock(Mock<UserManager<User>> mockedUserManager, User user, bool returns)
        {
            mockedUserManager.Setup(um => um.IsInRoleAsync(user, It.IsAny<string>()))
                .Returns(Task.FromResult(returns));
        }

        internal static void SetupUserManagerDeleteAsyncMock(Mock<UserManager<User>> mockedUserManager, User user)
        {
            mockedUserManager.Setup(um => um.DeleteAsync(user))
                .Returns(Task.FromResult(IdentityResult.Success));
        }
    }
}
