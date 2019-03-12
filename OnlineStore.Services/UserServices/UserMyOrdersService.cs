using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.OrderModels.ViewModels;
using OnlineStore.Services.UserServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OnlineStore.Services.UserServices
{
    public class UserMyOrdersService : BaseService, IUserMyOrdersService
    {
        private readonly IMapper mapper;

        public UserMyOrdersService(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public IEnumerable<MyOrderConciseViewModel> PrepareIndexModelForDisplaying(ClaimsPrincipal user)
        {
            var dbOrders = this.GetDbOrders(user);

            var models = this.mapper.Map<IEnumerable<MyOrderConciseViewModel>>(dbOrders);

            return models;
        }

        private IEnumerable<Order> GetDbOrders(ClaimsPrincipal user)
        {
            var dbUser = DbContext.Users
                .Where(u => u.UserName == user.Identity.Name)
                .First();

            var dbOrders = this.DbContext.Orders
                .Where(o => o.UserId == dbUser.Id)
                .Include(o => o.OrderStatus);

            return dbOrders;
        }
    }
}
