using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class EditAsyncTests : BaseAdminProductsServiceTest
    {
        [Theory]
        [InlineData("1")]
        public async Task WithIncorrectId_ShouldReturnFalse(string productId)
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var result = await service.EditAsync(null, productId);

            Assert.False(result);
        }

        [Fact]
        public async Task WithCorrectIdAndModel_ShouldReturnTrue()
        {
            var dbProduct = new Product();
            var model = new ProductBindingModel();

            var result = await this.CallEditAsync(dbProduct, model);

            Assert.True(result);
        }

        [Fact]
        public async Task WithCorrectIdAndModelWithIncorrectProductId_ShouldNotChangeProductIdInDatabase()
        {
            var dbProduct = new Product();
            var model = new ProductBindingModel()
            {
                ProductId = Guid.NewGuid().ToString()
            };

            var result = await this.CallEditAsync(dbProduct, model);
            var modelProductId = model.ProductId;
            var dbProductId = dbProduct.Id;

            Assert.NotEqual(modelProductId, dbProductId);
        }

        [Theory]
        [InlineData("oldName", "newName")]
        public async Task WithCorrectIdAndProductWithName_ShouldEditName(string oldName, string newName)
        {
            var dbProduct = new Product()
            {
                Name = oldName
            };
            var model = new ProductBindingModel()
            {
                ProductName = newName
            };

            await this.CallEditAsync(dbProduct, model);
            var currentDbProductName = dbProduct.Name;

            Assert.Equal(newName, currentDbProductName);
        }

        [Theory]
        [InlineData(1, 5)]
        public async Task WithCorrectIdAndProductWithPrice_ShouldEditPrice(decimal oldPrice, decimal newPrice)
        {
            var dbProduct = new Product()
            {
                Price = oldPrice
            };
            var model = new ProductBindingModel()
            {
                Price = newPrice
            };

            await this.CallEditAsync(dbProduct, model);
            var currentDbProductPrice = dbProduct.Price;

            Assert.Equal(newPrice, currentDbProductPrice);
        }

        [Theory]
        [InlineData(1, 5)]
        public async Task WithCorrectIdAndProductWithPromoPrice_ShouldEditPromoPrice(
            decimal oldPromoPrice, 
            decimal newPromoPrice)
        {
            var dbProduct = new Product()
            {
                PromoPrice = oldPromoPrice
            };
            var model = new ProductBindingModel()
            {
                PromoPrice = newPromoPrice
            };

            await this.CallEditAsync(dbProduct, model);
            var currentDbProductPromoPrice = dbProduct.PromoPrice;

            Assert.Equal(newPromoPrice, currentDbProductPromoPrice);
        }

        [Theory]
        [InlineData(1, 5)]
        public async Task WithCorrectIdAndProductWithCountsLeft_ShouldEditCountsLeft(
            int oldCountsLeft,
            int newCountsLeft)
        {
            var dbProduct = new Product()
            {
                CountsLeft = oldCountsLeft
            };
            var model = new ProductBindingModel()
            {
                CountsLeft = newCountsLeft
            };

            await this.CallEditAsync(dbProduct, model);
            var currentDbProductPromoPrice = dbProduct.CountsLeft;

            Assert.Equal(newCountsLeft, currentDbProductPromoPrice);
        }
        
        [Fact]
        public async Task WithCorrectIdAndProductWithDescription_ShouldEditDescription()
        {
            var oldDescription = Guid.NewGuid().ToString();
            var newDescriptin = Guid.NewGuid().ToString();
            
            var dbProduct = new Product()
            {
                Description = oldDescription
            };
            var model = new ProductBindingModel()
            {
                Description = newDescriptin
            };

            await this.CallEditAsync(dbProduct, model);
            var currentDbProductDescription = dbProduct.Description;

            Assert.Equal(newDescriptin, currentDbProductDescription);
        }

        [Fact]
        public async Task WithCorrectIdAndProductWithDescription_ShouldEditSpecifications()
        {
            var oldSpecifications = Guid.NewGuid().ToString();
            var newSpecifications = Guid.NewGuid().ToString();
            
            var dbProduct = new Product()
            {
                Specifications = oldSpecifications
            };
            var model = new ProductBindingModel()
            {
                Specifications = newSpecifications
            };

            await this.CallEditAsync(dbProduct, model);
            var currentDbProductSpecifications = dbProduct.Specifications;

            Assert.Equal(newSpecifications, currentDbProductSpecifications);
        }

        private async Task<bool> CallEditAsync(Product product, ProductBindingModel model)
        {
            var dbContext = this.GetDbContext();
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var result = await service.EditAsync(model, product.Id);

            return result;
        }
    }
}
