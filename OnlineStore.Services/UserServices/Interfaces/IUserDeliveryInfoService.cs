using OnlineStore.Models.WebModels.DeliveryInfo.BindingModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices.Interfaces
{
    public interface IUserDeliveryInfoService
    {
        DeliveryInfoBindingModel PrepareDeliveryInfoModelForAdding();

        Task AddDeliveryInfoToUserAsync(ClaimsPrincipal user, DeliveryInfoBindingModel model);

        DeliveryInfoBindingModel PrepareDeliveryInfoModelForEditing(ClaimsPrincipal user, string deliveryInfoId);

        Task<bool> EditDeliveryInfoAsync(ClaimsPrincipal user, DeliveryInfoBindingModel model, string deliveryInfoId);

        bool DeleteDeliveryInfo(ClaimsPrincipal user, string deliveryInfoID);

        Task<IEnumerable<string>> GetPopulatedPlacesByDistrictAsync(string districtId);
    }
}
