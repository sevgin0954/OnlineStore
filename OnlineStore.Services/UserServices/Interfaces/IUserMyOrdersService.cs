using OnlineStore.Models.WebModels.OrderModels.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices.Interfaces
{
    public interface IUserMyOrdersService
    {
        IEnumerable<MyOrderConciseViewModel> PrepareIndexModelForDisplaying(ClaimsPrincipal user);
    }
}
