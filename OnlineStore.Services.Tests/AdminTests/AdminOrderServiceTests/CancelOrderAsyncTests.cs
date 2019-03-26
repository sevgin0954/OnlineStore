using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminOrderServiceTests
{
    public class CancelOrderAsyncTests : BaseAdminOrderServiceTest
    {
        [Fact]
        public async Task WithNullOrderId_ShouldReturnFalse()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var result = await service.CancelOrderAsync(null);

            Assert.False(result);
        }

        [Theory]
        [InlineData("1")]
        public async Task WithIncorrectId_ShouldReturnFalse(string orderId)
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var result = await service.CancelOrderAsync(orderId);

            Assert.False(result);
        }

        [Fact]
        public async Task WithCorrectId_ShouldReturnTrue()
        {
            var dbContext = this.GetDbContext();
            this.SeedOrderStatuses(dbContext);
            var dbOrder = new Order();
            var dbOrderStatus = new OrderStatus();
            dbOrder.OrderStatus = dbOrderStatus;
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbOrderId = dbOrder.Id;
            var result = await service.CancelOrderAsync(dbOrderId);

            Assert.True(result);
        }

        [Fact]
        public async Task WithCanceledOrder_ShouldReturnFalse()
        {
            var dbContext = this.GetDbContext();
            var dbOrder = this.CreateOrder(WebConstants.OrderStatusCanceled);
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbOrderId = dbOrder.Id;
            var result = await service.CancelOrderAsync(dbOrderId);

            Assert.False(result);
        }

        [Fact]
        public async Task WithDeliveredOrder_ShouldReturnFalse()
        {
            var dbContext = this.GetDbContext();
            var dbOrder = this.CreateOrder(WebConstants.OrderStatusDelivered);
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbOrderId = dbOrder.Id;
            var result = await service.CancelOrderAsync(dbOrderId);

            Assert.False(result);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(5, 3)]
        public async Task WithValidOrderWithProduct_ShouldIncreaseProductCountInDatabase(
            int initialProductCount,
            int orderedProductsCount)
        {
            var dbContext = this.GetDbContext();
            this.SeedOrderStatuses(dbContext);
            var dbProduct = new Product()
            {
                CountsLeft = initialProductCount
            };
            var dbOrder = 
                this.CreateOrder(WebConstants.OrderStatusOnTheWay, dbProduct, orderedProductsCount);

            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbOrderId = dbOrder.Id;
            await service.CancelOrderAsync(dbOrderId);
            var expectedCount = initialProductCount + orderedProductsCount;
            var currentDbProductCount = dbProduct.CountsLeft;

            Assert.Equal(expectedCount, currentDbProductCount);
        }

        [Fact]
        public async Task WithValidOrder_ShouldChangeStatusToCanceled()
        {
            var dbContext = this.GetDbContext();
            var dbOrder = this.CreateOrder(WebConstants.OrderStatusOnTheWay);
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);
            
            await service.CancelOrderAsync(dbOrder.Id);
            var currentDbOrderStatusName = dbOrder.OrderStatus.Name;

            Assert.Equal(WebConstants.OrderStatusCanceled, currentDbOrderStatusName);
        }

        private Order CreateOrder(string orderStatus)
        {
            var dbOrderStatus = new OrderStatus()
            {
                Name = WebConstants.OrderStatusCanceled
            };
            var dbOrder = new Order()
            {
                OrderStatus = dbOrderStatus
            };

            return dbOrder;
        }

        private Order CreateOrder(string orderStatus, Product product, int orderedProductsCount)
        {
            var dbOrderStatus = new OrderStatus()
            {
                Name = orderStatus
            };
            var dbOrderProduct = new OrderProduct()
            {
                Product = product,
                Count = orderedProductsCount
            };
            var dbOrder = new Order()
            {
                OrderStatus = dbOrderStatus
            };
            dbOrder.OrderProducts.Add(dbOrderProduct);
            return dbOrder;
        }

        private void SeedOrderStatuses(OnlineStoreDbContext dbContext)
        {
            dbContext.OrdersStatuses.Add(new OrderStatus()
            {
                Name = WebConstants.OrderStatusCanceled
            });
        }
    }
}
