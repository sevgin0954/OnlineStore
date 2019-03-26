using AutoMapper;
using OnlineStore.Data;
using OnlineStore.Services.Admin;
using OnlineStore.Services.Admin.Interfaces;
using OnlineStore.Services.Tests.Common;

namespace OnlineStore.Services.Tests.AdminTests.AdminOrderServiceTests
{
    public abstract class BaseAdminOrderServiceTest : BaseTest
    {
        private readonly IMapper mapper;

        public BaseAdminOrderServiceTest()
        {
            this.mapper = CommonTest.GetAutoMapper();
        }

        public IAdminOrderService GetService(OnlineStoreDbContext dbContext)
        {
            var service = new AdminOrderService(dbContext, this.mapper);

            return service;
        }
    }
}
