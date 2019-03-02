using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data;
using OnlineStore.Models;
using AutoMapper;
using OnlineStore.Services.Extensions;
using OnlineStore.Services.Extensions.Interfaces;
using OnlineStore.Services.UserServices.Interfaces;
using OnlineStore.Services.UserServices;
using System;
using OnlineStore.Common.Constants;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnlineStore.Services.EmailServices;
using OnlineStore.Models.WebModels.Email;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;
using OnlineStore.Services.Quest;
using OnlineStore.Services.Quest.Interfaces;

namespace OnlineStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(WebConstants.ConnectionString)));

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<OnlineStoreDbContext>();

            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(WebConstants.SessionIdleTimeoutDays);
                })
                .AddFacebook(options => 
                {
                    options.AppId = Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
                    options.AppSecret = Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration.GetSection("ExternalAuthentication:Google:ClientId").Value;
                    options.ClientSecret = Configuration.GetSection("ExternalAuthentication:Google:ClientSecret").Value;
                });

            services.AddAntiforgery();

            services.AddAutoMapper();

            RegisterServiceLayer(services);

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

                options.User.RequireUniqueEmail = true;

                options.Password = new PasswordOptions()
                {
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 4,
                    RequireLowercase = false
                };

                options.SignIn.RequireConfirmedEmail = false;
            });

            services.Configure<MessageSenderOptions>(this.Configuration.GetSection("Email:SendGrid"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(WebConstants.SessionIdleTimeoutDays);
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddScoped<IApplicationBuilderSeedDbService, ApplicationBuilderSeedDbService>();

            services.AddScoped<IUserDeliveryInfoService, UserDeliveryInfoService>();

            services.AddScoped<IUserSecurityService, UserSecurityService>();

            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IAdminUsersService, AdminUsersService>();

            services.AddScoped<IAdminCategoriesService, AdminCategoriesService>();

            services.AddScoped<IAdminProductsServices, AdminProductsServices>();

            services.AddScoped<IQuestHomeServices, QuestHomeServices>();

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services.AddScoped<IUserOrderService, UserOrderService>();


            services.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}
