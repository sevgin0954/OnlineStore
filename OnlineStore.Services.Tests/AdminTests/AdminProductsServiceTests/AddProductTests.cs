using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class AddProductTests : BaseAdminProductsServiceTest
    {
        [Fact]
        public async Task WithModel_ShouldAddNewProductToDatabase()
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel();

            await this.CallAddProductAsync(dbContext, model);
            var dbProducts = dbContext.Products;
            var dbProductsCount = dbProducts.Count();

            Assert.Equal(1, dbProductsCount);
        }

        [Fact]
        public async Task WithModelWithSubCategoryId_ShouldAddNewProductToDatabaseWithCorrectSubCategoryId()
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            dbContext.SubCategories.Add(dbSubCategory);
            dbContext.SaveChanges();

            var model = new ProductBindingModel()
            {
                SubCategoryId = dbSubCategory.Id
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductSubCategoryId = dbFirstProduct.SubCategoryId;
            var modelSubCategoryId = model.SubCategoryId;

            Assert.Equal(modelSubCategoryId, dbProductSubCategoryId);
        }

        [Theory]
        [InlineData("ProductName")]
        public async Task WithModelWithProductName_ShouldAddNewProductToDatabaseWithCorrectProductName(string productName)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                ProductName = productName
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductName = dbFirstProduct.Name;

            Assert.Equal(productName, dbProductName);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithModelWithPrice_ShouldAddNewProductToDatabaseWithCorrectPrice(decimal price)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                Price = price
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductPrice = dbFirstProduct.Price;

            Assert.Equal(price, dbProductPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithModelWithPromoPrice_ShouldAddNewProductToDatabaseWithCorrectPromoPrice(decimal promoPrice)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                PromoPrice = promoPrice
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductPromoPrice = dbFirstProduct.PromoPrice;

            Assert.Equal(promoPrice, dbProductPromoPrice);
        }
        
        [Fact]
        public async Task WithModelWithoutPromoPrice_ShouldAddNewProductToDatabaseWithNullPromoPrice()
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel();

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductPromoPrice = dbFirstProduct.PromoPrice;

            Assert.Null(dbProductPromoPrice);
        }
        
        [Theory]
        [InlineData(5)]
        public async Task WithModelWithCountsLeft_ShouldAddNewProductToDatabaseWithCorrectCountsLeft(int coutsLeft)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                CountsLeft = coutsLeft
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductCountsLeft = dbFirstProduct.CountsLeft;

            Assert.Equal(coutsLeft, dbProductCountsLeft);
        }

        [Theory]
        [InlineData("Description")]
        public async Task WithModelWithDescription_ShouldAddNewProductToDatabaseWithCorrectDescription(string description)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                Description = description
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductDescription = dbFirstProduct.Description;

            Assert.Equal(description, dbProductDescription);
        }

        [Theory]
        [InlineData("Specifications")]
        public async Task WithModelWithSpecifications_ShouldAddNewProductToDatabaseWithCorrectSpecifications(
            string specifications)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                Specifications = specifications
            };

            await this.CallAddProductAsync(dbContext, model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductSpecifications = dbFirstProduct.Specifications;

            Assert.Equal(specifications, dbProductSpecifications);
        }

        private async Task CallAddProductAsync(OnlineStoreDbContext dbContext, ProductBindingModel model)
        {
            var service = this.GetService(dbContext);

            await service.AddProductAsync(model);
        }
    }
}
