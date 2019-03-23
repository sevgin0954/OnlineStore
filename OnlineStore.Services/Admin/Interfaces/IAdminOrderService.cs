using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin.Interfaces
{
    public interface IAdminOrderService
    {
        Task<IList<OrderConciseViewModel>> GetAllOrdersAsync();

        Task<bool> CancelOrderAsync(string orderId);
    }
}
