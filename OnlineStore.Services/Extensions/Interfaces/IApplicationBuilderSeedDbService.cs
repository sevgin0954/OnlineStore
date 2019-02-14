using Microsoft.AspNetCore.Identity;
using OnlineStore.Models;

namespace OnlineStore.Services.Extensions.Interfaces
{
    public interface IApplicationBuilderSeedDbService
    {
        void SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole> roleManager);
    }
}
