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
            var dbProduct = new Product();

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var dbProductId = dbProduct.Id;
            var productModelId = productModel.ProductId;

            Assert.Equal(dbProductId, productModelId);
        }

        [Fact]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectSubCategoryId()
        {
            var dbProduct = new Product();
            var dbSubCategory = new SubCategory();
            dbProduct.SubCategory = dbSubCategory;

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelSubCategoryId = productModel.SubCategoryId;
            var dbSubCategoryId = dbSubCategory.Id;

            Assert.Equal(dbSubCategoryId, productModelSubCategoryId);
        }

        [Theory]
        [InlineData("SubCategoryName")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectSubCategoryName(string subCategoryName)
        {
            var dbProduct = new Product();
            var dbSubCategory = new SubCategory()
            {
                Name = subCategoryName
            };
            dbProduct.SubCategory = dbSubCategory;

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelSubCategoryName = productModel.SubCategoryName;

            Assert.Equal(subCategoryName, productModelSubCategoryName);
        }

        [Theory]
        [InlineData("ProductName")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectProductName(string productName)
        {
            var dbProduct = new Product()
            {
                Name = productName
            };

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelProductName = productModel.ProductName;

            Assert.Equal(productName, productModelProductName);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectPrice(decimal price)
        {
            var dbProduct = new Product()
            {
                Price = price
            };

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelPrice = productModel.Price;

            Assert.Equal(price, productModelPrice);
        }

        [Fact]
        public async Task WithCorrectProductIdAndWithoutPromoPrice_ShouldReturnModelNullPromoPrice()
        {
            var dbProduct = new Product();

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelPromoPrice = productModel.PromoPrice;

            Assert.Null(productModelPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectPromoPrice(decimal promoPrice)
        {
            var dbProduct = new Product()
            {
                PromoPrice = promoPrice
            };

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelPromoPrice = productModel.PromoPrice;

            Assert.Equal(promoPrice, productModelPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectCountsLeft(int countsLeft)
        {
            var dbProduct = new Product()
            {
                CountsLeft = countsLeft
            };

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelCountsLeft = productModel.CountsLeft;

            Assert.Equal(countsLeft, productModelCountsLeft);
        }

        [Theory]
        [InlineData("description")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectDescription(string description)
        {
            var dbProduct = new Product()
            {
                Description = description
            };

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelDescription = productModel.Description;

            Assert.Equal(description, productModelDescription);
        }

        [Theory]
        [InlineData("Specification")]
        public async Task WithCorrectProductId_ShouldReturnModelWithCorrectSpecifications(string specification)
        {
            var dbProduct = new Product()
            {
                Specifications = specification
            };

            var productModel = await this.CallPreparedModelForEditingAsync(dbProduct);
            var productModelSpecifications = productModel.Specifications;

            Assert.Equal(specification, productModelSpecifications);
        }

        private async Task<ProductBindingModel> CallPreparedModelForEditingAsync(Product product)
        {
            var dbContext = this.GetDbContext();
            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);
            var productModel = await service.PrepareModelForEditingAsync(product.Id);

            return productModel;
        }
    }
}
