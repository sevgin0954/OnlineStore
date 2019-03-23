using System;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;

namespace OnlineStore.Services.Tests
{
    public abstract class BaseTest
    {
        public OnlineStoreDbContext GetDbContext()
        {
            var options = this.InitializeDbContextOptions();

            var context = new OnlineStoreDbContext(options);

            return context;
        }

        private DbContextOptions<OnlineStoreDbContext> InitializeDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return options;
        }
    }
}
