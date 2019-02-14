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
        private readonly IDictionary<string, string[]> districtsNamesPopulatedPlacesNames;
        private readonly IdentityRole[] roles = new IdentityRole[]
        {
            new IdentityRole(WebConstants.AdminRole)
        };

        public ApplicationBuilderSeedDbService(
            OnlineStoreDbContext dbContext, RoleManager<IdentityRole> roleManager)
            : base(dbContext)
        {
            this.districtsNamesPopulatedPlacesNames = new Dictionary<string, string[]>();

            SeedInitialData();
        }

        public void SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedDistricts();

            SeedRolesAsync(roleManager).Wait();

            SeedAdminAsync(userManager, roleManager).Wait();
        }

        private void SeedInitialData()
        {
            this.districtsNamesPopulatedPlacesNames["Смолян"] =
                            new string[] { "Рудозем", "Мадан", "Ловци (Мадан)", "Бреза (Рудозем)" };

            this.districtsNamesPopulatedPlacesNames["София"] =
                new string[] { "Алдомировци (Сливница)", "Априлово (Горна Малина)", "Банчовци (Ихтиман)" };
        }

        private void SeedDistricts()
        {
            var dbDistricts = this.DbContext.Districts;

            foreach (var districtName in districtsNamesPopulatedPlacesNames.Keys)
            {
                var ditrict = dbDistricts
                    .Include(d => d.PopulatedPlaces)
                    .FirstOrDefault(d => d.Name == districtName);

                if (ditrict == null)
                {
                    ditrict = new District() { Name = districtName };
                    dbDistricts.Add(ditrict);

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
            foreach (var role in roles)
            {
                string roleName = role.Name;

                if (await roleManager.RoleExistsAsync(roleName) == false)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var usersRoles = this.DbContext.UserRoles.ToArray();

            var adminRoleId = await roleManager.GetRoleIdAsync(this.roles[0]);

            if (usersRoles.Any(ur => ur.RoleId == adminRoleId) == false)
            {
                var dbAdmin = new User()
                {
                    UserName = WebConstants.DefaultAdminUsername,
                    Email = WebConstants.DefaultAdminEmail
                };

                await userManager.CreateAsync(dbAdmin, WebConstants.DefaultAdminPassword);
                await userManager.AddToRoleAsync(dbAdmin, roles[0].Name);
            }
        }
    }
}
