using AutoMapper;
using OnlineStore.Data;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;
using OnlineStore.Services.Tests.Common;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public abstract class BaseAdminProductsServiceTest : BaseTest
    {
        private readonly IMapper mapper;

        public BaseAdminProductsServiceTest()
        {
            this.mapper = CommonTest.GetAutoMapper();
        }

        public IAdminProductsService GetService(OnlineStoreDbContext dbContext)
        {
            var service = new AdminProductsService(dbContext, this.mapper);

            return service;
        }
    }
}
