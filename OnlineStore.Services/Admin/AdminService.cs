using Microsoft.AspNetCore.Identity;
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
        public AdminService(OnlineStoreDbContext dbContext, UserManager<User> userManager)
            : base(dbContext, userManager) { }

        public IndexViewModel PrepareIndexModelForEditing()
        {
            var model = new IndexViewModel
            {
                UserCount = this.CountTotalUsers(),

                NewUsersCount = 
                    this.FilterUsersByRegisterDate(DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)), DateTime.UtcNow)
                    .Count(),

                OrdersCount = this.CountTotalOrders(),

                NewOrdersCount = 
                    this.FilterOrdersByDate(DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)), DateTime.UtcNow)
                    .Count(),

                ProductsCount = this.CountTotalProducts()
            };

            return model;
        }

        public IEnumerable<User> FilterUsersByRegisterDate(DateTime startDate, DateTime endDate)
        {
            var users = this.UserManager.Users;

            var filteredUsers = users.Where(u => u.RegisterDate > startDate && u.RegisterDate < endDate);

            return filteredUsers;
        }

        public IEnumerable<Order> FilterOrdersByDate(DateTime startDate, DateTime endDate)
        {
            var orders = this.DbContext.Orders.ToList();

            var filteredOrders = orders.Where(o => o.OrderDate > startDate && o.OrderDate < endDate);

            return filteredOrders;
        }

        public long CountTotalUsers()
        {
            return this.UserManager.Users.LongCount();
        }

        public long CountTotalOrders()
        {
            return this.DbContext.Orders.LongCount();
        }

        public long CountTotalProducts()
        {
            return this.DbContext.Products.LongCount();
        }
    }
}
