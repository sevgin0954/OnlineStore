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
        public readonly IMapper mapper;
        public readonly UserManager<User> userManager;

        public UserProfileService(OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IndexViewModel> PrepareIndexForEditingAsync(ClaimsPrincipal user)
        {
            var dbUser = await GetUserFromDatabase(user.Identity.Name);

            if (dbUser == null)
            {
                return null;
            }

            var model = await MapUserModel(dbUser);

            return model;
        }

        public async Task UpdateProfilePictureAsync(ClaimsPrincipal user, IFormFile picture)
        {
            var dbUser = await this.userManager.GetUserAsync(user);

            using (var memoryStream = new MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                dbUser.ProfilePicture = new Photo() { Data = memoryStream.ToArray() };
            }

            DbContext.SaveChanges();
        }

        public async Task<PersonInfoBindingModel> PreparePersonInfoForEditingAsync(ClaimsPrincipal user)
        {
            var dbUser = await this.userManager.GetUserAsync(user);

            var model = this.mapper.Map<PersonInfoBindingModel>(dbUser);

            return model;
        }

        public async Task EditPersonInfoAsync(ClaimsPrincipal user, PersonInfoBindingModel model)
        {
            var dbUser = await this.userManager.GetUserAsync(user);

            this.mapper.Map(model, dbUser);

            await this.DbContext.SaveChangesAsync();
        }

        private IList<DeliveryInfoViewModel> MapDeliveryInfos(ICollection<DeliveryInfo> deliveryInfos)
        {
            var models = new List<DeliveryInfoViewModel>();

            foreach (var deliveryInfo in deliveryInfos)
            {
                var model = this.mapper.Map<DeliveryInfoViewModel>(deliveryInfo);

                model.SelectedDistrictName = deliveryInfo.District.Name;
                model.SelectedPopulatedName = deliveryInfo.PopulatedPlace.Name;

                models.Add(model);
            }

            return models;
        }

        private async Task<User> GetUserFromDatabase(string username)
        {
            return await this.DbContext.Users
                .Where(u => u.UserName == username)
                .Include(u => u.DeliveryInfos)
                    .ThenInclude(di => di.District)
                .Include(u => u.DeliveryInfos)
                    .ThenInclude(di => di.PopulatedPlace)
                .Include(u => u.ProfilePicture)
                .FirstOrDefaultAsync();
        }

        private async Task<IndexViewModel> MapUserModel(User source)
        {
            var model = new IndexViewModel()
            {
                FullName = source.FullName,
                DeliveryInfos = this.MapDeliveryInfos(source.DeliveryInfos),
                Email = await this.userManager.GetEmailAsync(source),
                PhoneNumber = await this.userManager.GetPhoneNumberAsync(source)
            };

            if (source.ProfilePicture != null)
            {
                model.ProfileImageAsByte = source.ProfilePicture.Data;
            }

            return model;
        }
    }
}
