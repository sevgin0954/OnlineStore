using AutoMapper;
using OnlineStore.Data;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;
using OnlineStore.Services.Tests.Common;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public abstract class BaseAdminCategoriesSeviceTest : BaseTest
    {
        private readonly IMapper mapper;

        public BaseAdminCategoriesSeviceTest()
        {
            this.mapper = CommonTest.GetAutoMapper();
        }

        public IAdminCategoriesService GetService(OnlineStoreDbContext dbContext)
        {
            var service = new AdminCategoriesService(dbContext, this.mapper);

            return service;
        }
    }
}
