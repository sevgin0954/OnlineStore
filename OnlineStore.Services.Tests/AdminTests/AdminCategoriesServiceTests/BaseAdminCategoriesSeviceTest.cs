using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;
using OnlineStore.Web.Mapping;
using System;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public abstract class BaseAdminCategoriesSeviceTest
    {
        private readonly IMapper mapper;

        public BaseAdminCategoriesSeviceTest()
        {
            this.mapper = this.InitializeAutoMapper();
        }

        public OnlineStoreDbContext GetDbContext()
        {
            var options = this.InitializeDbContextOptions();

            var context = new OnlineStoreDbContext(options);

            return context;
        }

        public IAdminCategoriesService GetService(OnlineStoreDbContext dbContext)
        {
            var service = new AdminCategoriesService(dbContext, this.mapper);

            return service;
        }

        private DbContextOptions<OnlineStoreDbContext> InitializeDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return options;
        }

        private IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
