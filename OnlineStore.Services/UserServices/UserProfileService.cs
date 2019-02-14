using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Account.BindingModels;
using OnlineStore.Models.WebModels.Account.ViewModels;
using OnlineStore.Models.WebModels.DeliveryInfo.ViewModels;
using OnlineStore.Services.UserServices.Interfaces;

namespace OnlineStore.Services.UserServices
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        public UserProfileService(OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager) { }

        public async Task<IndexViewModel> PrepareIndexForEditingAsync(ClaimsPrincipal user)
        {
            var dbUser = this.DbContext.Users
                .Where(u => u.UserName == user.Identity.Name)
                .Include(u => u.DeliveryInfos)
                    .ThenInclude(di => di.District)
                .Include(u => u.DeliveryInfos)
                    .ThenInclude(di => di.PopulatedPlace)
                .Include(u => u.ProfilePicture)
                .FirstOrDefault();

            if (dbUser == null)
            {
                return null;
            }

            var model = new IndexViewModel()
            {
                FullName = dbUser.FullName,
                DeliveryInfos = this.MapDeliveryInfos(dbUser.DeliveryInfos),
                Email = await this.UserManager.GetEmailAsync(dbUser),
                PhoneNumber = await this.UserManager.GetPhoneNumberAsync(dbUser)
            };

            if (dbUser.ProfilePicture != null)
            {
                model.ProfileImageAsByte = dbUser.ProfilePicture.Data;
            }

            return model;
        }

        public async Task UpdateProfilePictureAsync(ClaimsPrincipal user, IFormFile picture)
        {
            var dbUser = await this.UserManager.GetUserAsync(user);

            using (var memoryStream = new MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                dbUser.ProfilePicture = new Photo() { Data = memoryStream.ToArray() };
            }

            DbContext.SaveChanges();
        }

        public async Task<PersonInfoBindingModel> PreparePersonInfoForEditingAsync(ClaimsPrincipal user)
        {
            User dbUser = await this.UserManager.GetUserAsync(user);

            var model = this.Mapper.Map<PersonInfoBindingModel>(dbUser);

            return model;
        }

        public async Task EditPersonInfoAsync(ClaimsPrincipal user, PersonInfoBindingModel model)
        {
            var dbUser = await this.UserManager.GetUserAsync(user);

            this.Mapper.Map(model, dbUser);

            await this.DbContext.SaveChangesAsync();
        }

        private List<DeliveryInfoViewModel> MapDeliveryInfos(ICollection<DeliveryInfo> deliveryInfos)
        {
            List<DeliveryInfoViewModel> models = new List<DeliveryInfoViewModel>();

            foreach (var deliveryInfo in deliveryInfos)
            {
                var model = this.Mapper.Map<DeliveryInfoViewModel>(deliveryInfo);

                model.SelectedDistrictName = deliveryInfo.District.Name;
                model.SelectedPopulatedName = deliveryInfo.PopulatedPlace.Name;

                models.Add(model);
            }

            return models;
        }
    }
}
