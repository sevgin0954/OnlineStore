using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminServiceTests
{
    public class PrepareIndexModelForEditingTests : BaseAdminServiceTest
    {
        [Fact]
        public void WithTwoUser_ShouldReturnModelWithCorrectUserCount()
        {
            var dbUser1 = new User();
            var dbUser2 = new User();

            var model = this.CallPrepareIndexModelForEditingWithUsers(dbUser1, dbUser2);
            var modelUserCount = model.UserCount;

            Assert.Equal(2, modelUserCount);
        }

        [Fact]
        public void WithTwoNewUsers_ShouldReturnModelWithCorrectNewUsersCount()
        {
            var todayDateTime = DateTime.UtcNow;

            var dbUser1 = new User()
            {
                RegisterDate = todayDateTime
            };
            var dbUser2 = new User()
            {
                RegisterDate = todayDateTime
            };

            var model = this.CallPrepareIndexModelForEditingWithUsers(dbUser1, dbUser2);
            var modelNewUsersCount = model.NewUsersCount;

            Assert.Equal(2, modelNewUsersCount);
        }

        [Fact]
        public void WithOneOldUser_ShouldReturnModelWithZeroNewUsersCount()
        {
            var oldDateTime = this.CreateOldOrdersDateTime();

            var dbUser1 = new User()
            {
                RegisterDate = oldDateTime
            };

            var model = this.CallPrepareIndexModelForEditingWithUsers(dbUser1);
            var modelNewUsersCount = model.NewUsersCount;

            Assert.Equal(0, modelNewUsersCount);
        }

        [Fact]
        public void WithTwoOrders_ShouldReturnCorrectOrdersCount()
        {
            var dbOrder1 = new Order();
            var dbOrder2 = new Order();

            var model = this.CallPrepareIndexModelForEditingWithOrders(dbOrder1, dbOrder2);
            var modelOrdersCount = model.OrdersCount;

            Assert.Equal(2, modelOrdersCount);
        }

        [Fact]
        public void WithTwoNewOrders_ShouldReturnModelWithCorrectNewOrdersCount()
        {
            var todayDateTime = DateTime.UtcNow;

            var dbOrder1 = new Order()
            {
                OrderDate = todayDateTime
            };
            var dbOrder2 = new Order()
            {
                OrderDate = todayDateTime
            };

            var model = this.CallPrepareIndexModelForEditingWithOrders(dbOrder1, dbOrder2);
            var modelNewOrdersCount = model.NewOrdersCount;

            Assert.Equal(2, modelNewOrdersCount);
        }

        [Fact]
        public void WithOneOldOrder_ShouldReturnModelWithZoroNewOrdersCount()
        {
            var oldDateTime = this.CreateOldOrdersDateTime();

            var dbOrder1 = new Order()
            {
                OrderDate = oldDateTime
            };

            var model = this.CallPrepareIndexModelForEditingWithOrders(dbOrder1);
            var modelNewOrdersCount = model.NewOrdersCount;

            Assert.Equal(0, modelNewOrdersCount);
        }

        [Fact]
        public void WithTwoProducts_ShouldReturnModelWithCorrectProductsCount()
        {
            var dbProduct1 = new Product();
            var dbProduct2 = new Product();

            var model = this.CallPrepareIndexModelForEditingWithProducts(dbProduct1, dbProduct2);
            var modelProductsCount = model.ProductsCount;

            Assert.Equal(2, modelProductsCount);
        }

        private IndexViewModel CallPrepareIndexModelForEditingWithUsers(params User[] users)
        {
            var dbContext = this.GetDbContext();
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            var model = this.CallPrepareIndexModelForEditing(dbContext);
            return model;
        }

        private IndexViewModel CallPrepareIndexModelForEditingWithOrders(params Order[] orders)
        {
            var dbContext = this.GetDbContext();
            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
            
            var model = this.CallPrepareIndexModelForEditing(dbContext);
            return model;
        }

        private IndexViewModel CallPrepareIndexModelForEditingWithProducts(params Product[] products)
        {
            var dbContext = this.GetDbContext();
            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

            var model = this.CallPrepareIndexModelForEditing(dbContext);
            return model;
        }

        private IndexViewModel CallPrepareIndexModelForEditing(OnlineStoreDbContext dbContext)
        {
            var service = this.GetService(dbContext);
            var model = service.PrepareIndexModelForEditing();

            return model;
        }

        private DateTime CreateOldOrdersDateTime()
        {
            var oldDateTime = this.CreateOldDateTime(WebConstants.DaysPastToCountAsNewOrder);
            return oldDateTime;
        }

        private DateTime CreateOldUsersDateTime()
        {
            var oldDateTime = this.CreateOldDateTime(WebConstants.DaysPastToCountAsNewOrder);
            return oldDateTime;
        }

        private DateTime CreateOldDateTime(int daysPastToCountAsNew)
        {
            var oldDateTime = DateTime.UtcNow
                .Subtract(TimeSpan.FromDays(daysPastToCountAsNew))
                .Subtract(TimeSpan.FromDays(1));

            return oldDateTime;
        }
    }
}
