using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class PrepareModelForEditingTests : BaseAdminProductsServiceTest
    {
        [Theory]
        [InlineData("1")]
        public async Task WithIncorrectProductId_ShouldReturnNull(string productId)
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(productId);

            Assert.Null(productModel);
        }

        [Fact]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectProductId()
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product();
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbProductId = dbProduct.Id;
            var productModel = await service.PrepareModelForEditingAsync(dbProductId);
            var productModelId = productModel.ProductId;

            Assert.Equal(dbProductId, productModelId);
        }

        [Fact]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectSubCategoryId()
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product();
            var dbSubCategory = new SubCategory();

            var productModel = await this.GetPreparedModelForEditingAsync(dbContext, dbProduct, dbSubCategory);
            var productModelSubCategoryId = productModel.SubCategoryId;
            var dbSubCategoryId = dbSubCategory.Id;

            Assert.Equal(dbSubCategoryId, productModelSubCategoryId);
        }

        [Theory]
        [InlineData("SubCategoryName")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectSubCategoryName(string subCategoryName)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product();
            var dbSubCategory = new SubCategory()
            {
                Name = subCategoryName
            };

            var productModel = await this.GetPreparedModelForEditingAsync(dbContext, dbProduct, dbSubCategory);
            var productModelSubCategoryName = productModel.SubCategoryName;

            Assert.Equal(subCategoryName, productModelSubCategoryName);
        }

        [Theory]
        [InlineData("ProductName")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectProductName(string productName)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product()
            {
                Name = productName
            };
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelProductName = productModel.ProductName;

            Assert.Equal(productName, productModelProductName);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectPrice(decimal price)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product()
            {
                Price = price
            };
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelPrice = productModel.Price;

            Assert.Equal(price, productModelPrice);
        }

        [Fact]
        public async Task WithCorrectProductIdAndWithoutPromoPrice_ShouldReturnModelNullPromoPrice()
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product();
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelPromoPrice = productModel.PromoPrice;

            Assert.Null(productModelPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectPromoPrice(decimal promoPrice)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product()
            {
                PromoPrice = promoPrice
            };
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelPromoPrice = productModel.PromoPrice;

            Assert.Equal(promoPrice, productModelPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectCountsLeft(int countsLeft)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product()
            {
                CountsLeft = countsLeft
            };
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelCountsLeft = productModel.CountsLeft;

            Assert.Equal(countsLeft, productModelCountsLeft);
        }

        [Theory]
        [InlineData("description")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectDescription(string description)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product()
            {
                Description = description
            };
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelDescription = productModel.Description;

            Assert.Equal(description, productModelDescription);
        }

        [Theory]
        [InlineData("specification")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectSpecifications(string specification)
        {
            var dbContext = this.GetDbContext();
            var dbProduct = new Product()
            {
                Specifications = specification
            };
            dbContext.Products.Add(dbProduct);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var productModel = await service.PrepareModelForEditingAsync(dbProduct.Id);
            var productModelSpecifications = productModel.Specifications;

            Assert.Equal(specification, productModelSpecifications);
        }

        private async Task<ProductBindingModel> GetPreparedModelForEditingAsync(
            OnlineStoreDbContext dbContext,
            Product product,
            SubCategory subCategory)
        {
            subCategory.Products.Add(product);
            dbContext.SubCategories.Add(subCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);
            var productModel = await service.PrepareModelForEditingAsync(product.Id);

            return productModel;
        }
    }
}
