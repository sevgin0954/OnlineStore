using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(OnlineStore.Web.Areas.Identity.IdentityHostingStartup))]
namespace OnlineStore.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}