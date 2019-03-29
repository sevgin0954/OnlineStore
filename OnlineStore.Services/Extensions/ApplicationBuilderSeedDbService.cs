using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Services.Extensions.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Extensions
{
    public class ApplicationBuilderSeedDbService : BaseService, IApplicationBuilderSeedDbService
    {
        private readonly IList<string> paymentsTypesNames;
        private readonly IList<string> orderStatusesNames;
        private readonly IDictionary<string, string[]> districtsNamesPopulatedPlacesNames;
        private readonly IList<string> rolesNames;

        public ApplicationBuilderSeedDbService(OnlineStoreDbContext dbContext)
            : base(dbContext)
        {
            this.paymentsTypesNames = new List<string>();

            this.orderStatusesNames = new List<string>();

            this.districtsNamesPopulatedPlacesNames = new Dictionary<string, string[]>();

            this.rolesNames = new List<string>();

            SeedInitialData();
        }

        public void SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedDistricts();

            SeedRolesAsync(roleManager).Wait();

            SeedAdminAsync(userManager, roleManager).Wait();

            SeedPaymentTypes();

            SeedOrderStatuses();
        }

        private void SeedInitialData()
        {
            this.districtsNamesPopulatedPlacesNames["Смолян"] =
                            new string[] { "Рудозем", "Мадан", "Ловци (Мадан)", "Бреза (Рудозем)" };

            this.districtsNamesPopulatedPlacesNames["София"] =
                new string[] { "Алдомировци (Сливница)", "Априлово (Горна Малина)", "Банчовци (Ихтиман)" };

            this.paymentsTypesNames.Add("Cash on delivery");

            this.orderStatusesNames.Add(WebConstants.OrderStatusOnTheWay);
            this.orderStatusesNames.Add(WebConstants.OrderStatusCanceled);
            this.orderStatusesNames.Add(WebConstants.OrderStatusDelivered);

            this.rolesNames.Add(WebConstants.AdminRoleName);
        }

        private void SeedDistricts()
        {
            var dbDistricts = this.DbContext.Districts
                .Include(d => d.PopulatedPlaces)
                .ToArray();

            foreach (var districtName in districtsNamesPopulatedPlacesNames.Keys)
            {
                var ditrict = dbDistricts
                    .FirstOrDefault(d => d.Name == districtName);

                if (ditrict == null)
                {
                    ditrict = new District() { Name = districtName };
                    this.DbContext.Districts.Add(ditrict);

                    SeedPopulatedPlace(ditrict);
                }

                SeedPopulatedPlace(ditrict);
            }

            DbContext.SaveChanges();
        }

        private void SeedPopulatedPlace(District district)
        {
            var dbPopulatedPlaces = district.PopulatedPlaces;

            foreach (var populatedPlaceName in this.districtsNamesPopulatedPlacesNames[district.Name])
            {
                if (dbPopulatedPlaces.Any(pp => pp.Name == populatedPlaceName) == false)
                {
                    dbPopulatedPlaces.Add(new PopulatedPlace() { Name = populatedPlaceName, DistrictId = district.Id });
                }
            }

            DbContext.SaveChanges();
        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in rolesNames)
            {
                if (await roleManager.RoleExistsAsync(roleName) == false)
                {
                    var role = new IdentityRole()
                    {
                        Name = roleName
                    };

                    await roleManager.CreateAsync(role);
                }
            }
        }

        private async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var usersRoles = this.DbContext.UserRoles
                .ToArray();

            var adminRole = await roleManager.FindByNameAsync(this.rolesNames[0]);

            if (usersRoles.Any(ur => ur.RoleId == adminRole.Id) == false)
            {
                var dbAdmin = new User()
                {
                    UserName = WebConstants.DefaultAdminUsername,
                    Email = WebConstants.DefaultAdminEmail
                };

                await userManager.CreateAsync(dbAdmin, WebConstants.DefaultAdminPassword);
                await userManager.AddToRoleAsync(dbAdmin, adminRole.Name);
            }
        }

        private void SeedPaymentTypes()
        {
            var dbPaymentTypes = this.DbContext.PaymentTypes
                .ToArray();

            foreach (var paymentTypeName in this.paymentsTypesNames)
            {
                if (dbPaymentTypes.Any(pt => pt.Name == paymentTypeName) == false)
                {
                    var dbPaymentType = new PaymentType()
                    {
                        Name = paymentTypeName
                    };

                    this.DbContext.PaymentTypes.Add(dbPaymentType);
                }
            }

            this.DbContext.SaveChanges();
        }

        private void SeedOrderStatuses()
        {
            var dbOrderStatuses = this.DbContext.OrdersStatuses
                .ToArray();

            foreach (var orderStatusName in this.orderStatusesNames)
            {
                if (dbOrderStatuses.Any(os => os.Name == orderStatusName) == false)
                {
                    var dbOrderStatus = new OrderStatus()
                    {
                        Name = orderStatusName
                    };

                    this.DbContext.OrdersStatuses.Add(dbOrderStatus);
                }
            }

            this.DbContext.SaveChanges();
        }
    }
}
