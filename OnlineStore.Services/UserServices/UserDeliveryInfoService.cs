using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.DeliveryInfo.BindingModels;
using OnlineStore.Services.UserServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices
{
    public class UserDeliveryInfoService : BaseService, IUserDeliveryInfoService
    {
        public readonly IMapper mapper;
        public readonly UserManager<User> userManager;

        public UserDeliveryInfoService(
            OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public DeliveryInfoBindingModel PrepareDeliveryInfoModelForAdding()
        {
            var model = new DeliveryInfoBindingModel();
            this.PopulateSelectLists(model);

            return model;
        }

        public async Task AddDeliveryInfoToUserAsync(ClaimsPrincipal user, DeliveryInfoBindingModel model)
        {
            var dbUser = await this.userManager.GetUserAsync(user);
            var deliveryInfoDbModel = this.mapper.Map<DeliveryInfo>(model);
            
            dbUser.DeliveryInfos.Add(deliveryInfoDbModel);

            this.DbContext.SaveChanges();
        }

        public DeliveryInfoBindingModel PrepareDeliveryInfoModelForEditing(ClaimsPrincipal user, string deliveryInfoId)
        {
            var deliveryInfoDbModel = this.GetDeliveryInfoFromUser(user, deliveryInfoId);

            if (deliveryInfoDbModel == null)
            {
                return null;
            }

            var deliveryInfoModel = this.mapper.Map<DeliveryInfoBindingModel>(deliveryInfoDbModel);

            var dbDistrict = this.DbContext.Districts.Find(deliveryInfoModel.SelectedDistrictId);
            var dbPopulatedPlace = this.DbContext.PopulatedPlaces.Find(deliveryInfoModel.SelectedPopulatedPlaceId);

            this.PopulateSelectLists(deliveryInfoModel, dbDistrict, dbPopulatedPlace);

            return deliveryInfoModel;
        }

        public async Task<bool> EditDeliveryInfoAsync(ClaimsPrincipal user, DeliveryInfoBindingModel model, string deliveryInfoId)
        {
            var deliveryInfoModel = GetDeliveryInfoFromUser(user, deliveryInfoId);

            if (deliveryInfoModel == null)
            {
                return false;
            }

            this.mapper.Map(model, deliveryInfoModel);

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public bool DeleteDeliveryInfo(ClaimsPrincipal user, string deliveryInfoID)
        {
            var deliveryInfoDbModel = this.GetDeliveryInfoFromUser(user, deliveryInfoID);

            if (deliveryInfoDbModel == null)
            {
                return false;
            }

            this.DbContext.DeliverysInfos.Remove(deliveryInfoDbModel);

            this.DbContext.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<string>> GetPopulatedPlacesByDistrictAsync(string districtId)
        {
            var dbDistrict = await this.DbContext.Districts
                .Include(d => d.PopulatedPlaces)
                .FirstOrDefaultAsync(d => d.Id == districtId);

            if (dbDistrict == null)
            {
                return null;
            }

            var populatedPlacesIds = dbDistrict.PopulatedPlaces.Select(pp => pp.Id);

            return populatedPlacesIds;
        }

        private void PopulateSelectLists(
            DeliveryInfoBindingModel model, 
            District selectedDistrict = null, 
            PopulatedPlace selectedPopulatedPlace = null)
        {
            model.AllDistricts = GetDistrictsAsSelectList();

            model.AllPopulatedPlaces = GetPopulatedPlacesAsSelectList();

            AddDefaultPopulatedPlace(model.AllPopulatedPlaces);

            if (selectedDistrict == null || selectedPopulatedPlace == null)
            {
                AddDefaultDistrict(model.AllDistricts);
            }
            else
            {
                model.AllDistricts
                    .First(d => d.Value == selectedDistrict.Id).Selected = true;

                model.AllPopulatedPlaces
                    .First(pp => pp.Value == selectedPopulatedPlace.Id).Selected = true;
            }
        }

        private DeliveryInfo GetDeliveryInfoFromUser(ClaimsPrincipal user, string deliveryInfoId)
        {
            var dbUser = this.DbContext.Users
                .Include(u => u.DeliveryInfos)
                .FirstOrDefault(u => u.UserName == user.Identity.Name);

            if (dbUser == null)
            {
                return null;
            }

            var deliveryInfoDbModel = dbUser.DeliveryInfos
                .FirstOrDefault(di => di.Id == deliveryInfoId);

            return deliveryInfoDbModel;
        }

        private ICollection<SelectListItem> GetDistrictsAsSelectList()
        {
            return this.DbContext
                    .Districts
                    .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id, Selected = false })
                    .ToList();
        }

        private ICollection<SelectListItem> GetPopulatedPlacesAsSelectList()
        {
            return this.DbContext
                .PopulatedPlaces
                .Select(pp => new SelectListItem() { Text = pp.Name, Value = pp.Id, Selected = false })
                .ToList();
        }

        private void AddDefaultPopulatedPlace(ICollection<SelectListItem> populatedPlaces)
        {
            populatedPlaces
                .Add(new SelectListItem()
                {
                    Text = ControllerConstats.FromPlaceholderPopulatedPlace,
                    Selected = true,
                    Disabled = true
                });
        }

        private void AddDefaultDistrict(ICollection<SelectListItem> districts)
        {
            districts
                .Add(new SelectListItem()
                {
                    Text = ControllerConstats.FromPlaceholderDistrict,
                    Selected = true,
                    Disabled = true
                });
        }
    }
}
