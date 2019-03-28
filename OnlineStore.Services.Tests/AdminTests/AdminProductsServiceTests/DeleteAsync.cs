using OnlineStore.Data;
using OnlineStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class DeleteAsync : BaseAdminProductsServiceTest
    {
        [Fact]
        public async Task WithIncorrectProductId_ShouldReturnFalse()
        {
            var incorrectProductId = Guid.NewGuid().ToString();

            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);


            var result = await service.DeleteAsync(incorrectProductId);

            Assert.False(result);
        }

        [Fact]
        public async Task WithCorrectProductId_ShouldReturnTrue()
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product();

            var result = await this.CallDeleteAsync(dbContext, dbProduct);

            Assert.True(result);
        }

        [Fact]
        public async Task WithCorrectProductId_ShouldRemoveProductFromDatabase()
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product();

            await this.CallDeleteAsync(dbContext, dbProduct);

            var dbProductsCount = dbContext.Products.Count();

            Assert.Equal(0, dbProductsCount);
        }

        [Fact]
        public async Task WithCorrectProductId_ShouldRemoveProductPhotosFromDatabase()
        {
            var dbContext = this.GetDbContext();
            var dbPhoto = new Photo();
            var dbProduct = new Product();
            dbProduct.Photos.Add(dbPhoto);

            await this.CallDeleteAsync(dbContext, dbProduct);
            
            var dbProductPhotosCount = dbContext.Photos.Count();

            Assert.Equal(0, dbProductPhotosCount);
        }

        private async Task<bool> CallDeleteAsync(OnlineStoreDbContext dbContext, Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);
            var result = await service.DeleteAsync(product.Id);

            return result;
        }
    }
}
