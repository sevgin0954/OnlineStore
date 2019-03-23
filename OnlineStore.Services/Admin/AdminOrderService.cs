using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin
{
    public class AdminOrderService : BaseService, IAdminOrderService
    {
        private readonly IMapper mapper;

        public AdminOrderService(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IList<OrderConciseViewModel>> GetAllOrdersAsync()
        {
            var dbOrders = await this.GetOrdersFromDatabaseAsync();

            var models = this.mapper.Map<IList<OrderConciseViewModel>>(dbOrders);

            return models;
        }

        public async Task<bool> CancelOrderAsync(string orderId)
        {
            var dbOrder = await this.GetOrderFromDatabaseAsync(orderId);

            if (dbOrder == null)
            {
                return false;
            }

            bool isOrderCanceled = this.CheckOrderStatus(dbOrder, WebConstants.OrderStatusCanceled);
            if (isOrderCanceled)
            {
                return false;
            }

            bool isOrderDelivered = this.CheckOrderStatus(dbOrder, WebConstants.OrderStatusDelivered);
            if (isOrderDelivered == false)
            {
                await this.AddProductsFromOrdersToDatabaseAsync(dbOrder.OrderProducts);
            }

            await this.ChangeOrderStatusToCanceledAsync(dbOrder);

            return true;
        }

        private async Task<IEnumerable<Order>> GetOrdersFromDatabaseAsync()
        {
            var dbOrders = await this.DbContext.Orders
                .Include(o => o.OrderStatus)
                .ToArrayAsync();

            return dbOrders;
        }

        private async Task<Order> GetOrderFromDatabaseAsync(string orderId)
        {
            var dbOrder = await this.DbContext.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return dbOrder;
        }

        private bool CheckOrderStatus(Order order, string orderStatusDelivered)
        {
            return order.OrderStatus.Name == orderStatusDelivered;
        }

        private async Task AddProductsFromOrdersToDatabaseAsync(ICollection<OrderProduct> orders)
        {
            foreach (var order in orders)
            {
                var dbProduct = await this.DbContext.Products
                    .FindAsync(order.ProductId);

                if (dbProduct == null)
                {
                    continue;
                }

                dbProduct.CountsLeft += order.Count;
            }

            await this.DbContext.SaveChangesAsync();
        }

        private async Task ChangeOrderStatusToCanceledAsync(Order order)
        {
            var dbOrderStatusCanceled = await this.DbContext.OrdersStatuses
                .Where(os => os.Name == WebConstants.OrderStatusCanceled)
                .FirstAsync();

            order.OrderStatusId = dbOrderStatusCanceled.Id;
            await this.DbContext.SaveChangesAsync();
        }
    }
}
