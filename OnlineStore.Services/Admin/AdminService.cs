using Microsoft.AspNetCore.Identity;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Services.Admin
{
    public class AdminService : BaseService, IAdminService
    {
        public readonly UserManager<User> userManager;

        public AdminService(OnlineStoreDbContext dbContext, UserManager<User> userManager)
            : base(dbContext)
        {
            this.userManager = userManager;
        }

        public IndexViewModel PrepareIndexModelForEditing()
        {
            var usersStartDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(WebConstants.DaysPastToCountAsNewUser));
            var ordersStartDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(WebConstants.DaysPastToCountAsNewOrder));
            var endDateTime = DateTime.UtcNow;

            var model = new IndexViewModel
            {
                UserCount = this.CountTotalUsers(),

                NewUsersCount = 
                    this.FilterUsersByRegisterDate(usersStartDateTime, endDateTime)
                    .Count(),

                OrdersCount = this.CountTotalOrders(),

                NewOrdersCount = 
                    this.FilterOrdersByDate(ordersStartDateTime, endDateTime)
                    .Count(),

                ProductsCount = this.CountTotalProducts()
            };

            return model;
        }

        public IEnumerable<User> FilterUsersByRegisterDate(DateTime startDate, DateTime endDate)
        {
            var users = this.userManager.Users;

            var filteredUsers = users.Where(
                u => DateTime.Compare(u.RegisterDate.Date, startDate.Date) >= 0 &&
                DateTime.Compare(u.RegisterDate.Date, endDate.Date) <= 0);

            return filteredUsers;
        }

        public IEnumerable<Order> FilterOrdersByDate(DateTime startDate, DateTime endDate)
        {
            var orders = this.DbContext.Orders.ToList();

            var filteredOrders = orders.Where(
                o => DateTime.Compare(o.OrderDate.Date, startDate.Date) >= 0 &&
                DateTime.Compare(o.OrderDate.Date, endDate.Date) <= 0);

            return filteredOrders;
        }

        private long CountTotalUsers()
        {
            return this.userManager.Users.LongCount();
        }

        private long CountTotalOrders()
        {
            return this.DbContext.Orders.LongCount();
        }

        private long CountTotalProducts()
        {
            return this.DbContext.Products.LongCount();
        }
    }
}
