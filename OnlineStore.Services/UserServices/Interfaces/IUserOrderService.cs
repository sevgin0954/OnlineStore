using Microsoft.AspNetCore.Http;
using OnlineStore.Models.WebModels.OrderModels.BindingModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices.Interfaces
{
    public interface IUserOrderService
    {
        Task<IEnumerable<DeliveryInfoBindingModel>> PrepareModelForChoosingAddressAsync(ISession session, ClaimsPrincipal user);

        Task<OrderBidningModel> PrepareModelForOrdering(ClaimsPrincipal user, ISession session, string deliveryInfoId);

        Task<bool> CreateOrder(OrderBidningModel model, ClaimsPrincipal user, ISession session);
    }
}
