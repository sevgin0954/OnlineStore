using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminOrderServiceTests
{
    public class GetAllOrdersAsyncTests : BaseAdminOrderServiceTest
    {
        [Fact]
        public async Task WithOneOrder_ShouldReturnOneOrderModel()
        {
            var dbContext = this.GetDbContext();
            var dbOrder = new Order();
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var orderModels = await service.GetAllOrdersAsync();
            var modelsCount = orderModels.Count;

            Assert.Equal(1, modelsCount);
        }

        [Fact]
        public async Task WithoutOrders_ShouldReturnZeroModelCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var orderModels = await service.GetAllOrdersAsync();
            var modelsCount = orderModels.Count;

            Assert.Equal(0, modelsCount);
        }

        [Fact]
        public async Task WithOrderWithId_ShouldReturnModelWithCorrectId()
        {
            var dbContext = this.GetDbContext();
            var dbOrder = new Order();
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var firstOrderModel = await this.GetFirstOrderModelAsync(dbContext);
            var dbOrderId = dbOrder.Id;
            var orderModelId = firstOrderModel.Id;

            Assert.Equal(dbOrderId, orderModelId);
        }
        
        [Fact]
        public async Task WithOrderWithUserId_ShouldReturnModelWithCorrectUserId()
        {
            var dbContext = this.GetDbContext();
            var dbOrder = new Order();
            var dbUser = new User();
            dbOrder.User = dbUser;
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var firstOrderModel = await this.GetFirstOrderModelAsync(dbContext);
            var dbUserId = dbUser.Id;
            var modelUserId = firstOrderModel.UserId;

            Assert.Equal(dbUserId, modelUserId);
        }

        [Theory]
        [InlineData(1)]
        public async Task WithOrderWithTotalPrice_ShouldReturnModelWithCorrectTotalPrice(decimal totalPrice)
        {
            var dbContext = this.GetDbContext();
            var dbOrder = new Order
            {
                TotalPrice = totalPrice
            };
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();

            var firstOrderModel = await this.GetFirstOrderModelAsync(dbContext);
            var modelTotalPrice = firstOrderModel.TotalPrice;

            Assert.Equal(totalPrice, modelTotalPrice);
        }

        [Theory]
        [InlineData(1)]
        public async Task WithOrderWithDeliveryPrice_ShouldReturnModelWithCorrectDeliveryPrice(decimal deliveryPrice)
        {
            var dbContext = this.GetDbContext();
            var dbOrder = new Order
            {
                DeliveryPrice = deliveryPrice
            };
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();
            
            var firstOrderModel = await this.GetFirstOrderModelAsync(dbContext);
            var modelDeliveryPrice = firstOrderModel.DeliveryPrice;

            Assert.Equal(deliveryPrice, modelDeliveryPrice);
        }

        [Theory]
        [InlineData("delivered")]
        public async Task WithOrderWithOrderStatusName_ShouldReturnModelWithCorrectOrderStatusName(string statusName)
        {
            var dbContext = this.GetDbContext();
            var dbOrderStatus = new OrderStatus()
            {
                Name = statusName
            };
            var dbOrder = new Order()
            {
                OrderStatus = dbOrderStatus
            };
            dbContext.Orders.Add(dbOrder);
            dbContext.SaveChanges();
            
            var firstOrderModel = await this.GetFirstOrderModelAsync(dbContext);
            var modelStatusName = firstOrderModel.OrderStatusName;

            Assert.Equal(statusName, modelStatusName);
        }

        private async Task<OrderConciseViewModel> GetFirstOrderModelAsync(OnlineStoreDbContext dbContext)
        {
            var service = this.GetService(dbContext);

            var orderModels = await service.GetAllOrdersAsync();
            var firstOrderModel = orderModels.First();

            return firstOrderModel;
        }
    }
}
