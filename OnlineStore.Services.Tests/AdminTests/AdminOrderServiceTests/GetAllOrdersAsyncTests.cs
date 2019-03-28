using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Collections.Generic;
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
            var dbOrder = new Order();

            var orderModels = await this.CallGetAllOrdersAsync(dbOrder);
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
            var dbOrder = new Order();

            var orderModels = await this.CallGetAllOrdersAsync(dbOrder);
            var firstOrderModel = orderModels.First();
            var dbOrderId = dbOrder.Id;
            var orderModelId = firstOrderModel.Id;

            Assert.Equal(dbOrderId, orderModelId);
        }
        
        [Fact]
        public async Task WithOrderWithUserId_ShouldReturnModelWithCorrectUserId()
        {
            var dbOrder = new Order();
            var dbUser = new User();
            dbOrder.User = dbUser;

            var orderModels = await this.CallGetAllOrdersAsync(dbOrder);
            var firstOrderModel = orderModels.First();
            var dbUserId = dbUser.Id;
            var modelUserId = firstOrderModel.UserId;

            Assert.Equal(dbUserId, modelUserId);
        }

        [Theory]
        [InlineData(1)]
        public async Task WithOrderWithTotalPrice_ShouldReturnModelWithCorrectTotalPrice(decimal totalPrice)
        {
            var dbOrder = new Order
            {
                TotalPrice = totalPrice
            };

            var orderModels = await this.CallGetAllOrdersAsync(dbOrder);
            var firstOrderModel = orderModels.First();
            var modelTotalPrice = firstOrderModel.TotalPrice;

            Assert.Equal(totalPrice, modelTotalPrice);
        }

        [Theory]
        [InlineData(1)]
        public async Task WithOrderWithDeliveryPrice_ShouldReturnModelWithCorrectDeliveryPrice(decimal deliveryPrice)
        {
            var dbOrder = new Order
            {
                DeliveryPrice = deliveryPrice
            };

            var orderModels = await this.CallGetAllOrdersAsync(dbOrder);
            var firstOrderModel = orderModels.First();
            var modelDeliveryPrice = firstOrderModel.DeliveryPrice;

            Assert.Equal(deliveryPrice, modelDeliveryPrice);
        }

        [Theory]
        [InlineData("delivered")]
        public async Task WithOrderWithOrderStatusName_ShouldReturnModelWithCorrectOrderStatusName(string statusName)
        {
            var dbOrderStatus = new OrderStatus()
            {
                Name = statusName
            };
            var dbOrder = new Order()
            {
                OrderStatus = dbOrderStatus
            };
            
            var orderModels = await this.CallGetAllOrdersAsync(dbOrder);
            var firstOrderModel = orderModels.First();
            var modelStatusName = firstOrderModel.OrderStatusName;

            Assert.Equal(statusName, modelStatusName);
        }

        private async Task<IList<OrderConciseViewModel>> CallGetAllOrdersAsync(Order order)
        {
            var dbContext = this.GetDbContext();
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);
            var orderModels = await service.GetAllOrdersAsync();

            return orderModels;
        }
    }
}
