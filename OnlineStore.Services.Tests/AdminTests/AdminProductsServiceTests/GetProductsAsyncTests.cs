using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class GetProductsAsyncTests : BaseAdminProductsServiceTest
    {
        [Theory]
        [InlineData("1")]
        public async Task WithoutIncorrectSubcategoryId_ShouldReturnNull(string subcategoryId)
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var productModels = await service.GetProductsAsync(subcategoryId);

            Assert.Null(productModels);
        }

        [Fact]
        public async Task WithCorrectSubcategoryIdAndZeroProducts_ShouldReturnEmptyCollection()
        {
            var dbConctext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            dbConctext.SubCategories.Add(dbSubCategory);
            dbConctext.SaveChanges();

            var service = this.GetService(dbConctext);

            var productModels = await service.GetProductsAsync(dbSubCategory.Id);
            var productModelsCount = productModels.Count();

            Assert.Equal(0, productModelsCount);
        }

        [Fact]
        public async Task WithCorrectSubcategoryIdAndOneProductWithId_ShouldReturnModelWithCorrectId()
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product();

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelId = firstProductModel.Id;
            var dbProductId = dbProduct.Id;

            Assert.Equal(dbProductId, firstProductModelId);
        }

        [Theory]
        [InlineData("ProductName")]
        public async Task WithCorrectSubcategoryIdAndOneProductWithName_ShouldReturnModelWithCorrectName(string productName)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product()
            {
                Name = productName
            };

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelName = firstProductModel.Name;

            Assert.Equal(productName, firstProductModelName);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectSubcategoryIdAndOneProductWithPrice_ShouldReturnModelWithCorrectPrice(decimal productPrice)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product()
            {
                Price = productPrice
            };

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelPrice = firstProductModel.Price;

            Assert.Equal(productPrice, firstProductModelPrice);
        }

        [Fact]
        public async Task WithCorrectSubcategoryIdAndOneProductWithoutPromoPrice_ShouldReturnModelWithNullPromoPrice()
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product();

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelPromoPrice = firstProductModel.PromoPrice;

            Assert.Null(firstProductModelPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectSubcategoryIdAndOneProductWithPromoPrice_ShouldReturnModelWithCorrectPromoPrice(
            decimal promoPrice)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product()
            {
                PromoPrice = promoPrice
            };

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelPromoPrice = firstProductModel.PromoPrice;

            Assert.Equal(promoPrice, firstProductModelPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public async Task WithCorrectSubcategoryIdAndOneProductWithCountsLeft_ShouldReturnModelWithCorrectCountsLeft(
            int countsLeft)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product()
            {
                CountsLeft = countsLeft
            };

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelCountsLeft = firstProductModel.CountsLeft;

            Assert.Equal(countsLeft, firstProductModelCountsLeft);
        }

        [Fact]
        public async Task WithCorrectSubcategoryIdAndOneProductWithoutOrders_ShouldReturnModelWithZeroOrdersCount()
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbProduct = new Product();

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelOrdersCount = firstProductModel.OrdersCount;

            Assert.Equal(0, firstProductModelOrdersCount);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(1)]
        public async Task WithCorrectSubcategoryIdAndOneProductWithOrderWithOrderCount_ShouldReturnModelWithCorrectOrdersCount(
            int orderCount)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            var dbOrderProduct = new OrderProduct()
            {
                Count = orderCount
            };
            var dbProduct = new Product();
            dbProduct.Orders.Add(dbOrderProduct);

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelOrdersCount = firstProductModel.OrdersCount;

            Assert.Equal(orderCount, firstProductModelOrdersCount);
        }

        [Theory]
        [InlineData("SubCategoryName")]
        public async Task WithCorrectSubcategoryIdAndNameAndOneProduct_ShouldReturnModelWithCorrectSubCategoryName(
            string subCategoryName)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory()
            {
                Name = subCategoryName
            };
            var dbProduct = new Product();

            var productModels = await this.GetProductModelsFromService(dbContext, dbSubCategory, dbProduct);
            var firstProductModel = productModels.First();
            var firstProductModelSubCategoryName = firstProductModel.SubCategoryName;

            Assert.Equal(subCategoryName, firstProductModelSubCategoryName);
        }

        private async Task<IEnumerable<ProductViewModel>> GetProductModelsFromService(
            OnlineStoreDbContext dbContext,
            SubCategory subCateogory,
            Product product)
        {
            subCateogory.Products.Add(product);
            dbContext.SubCategories.Add(subCateogory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);
            var productModels = await service.GetProductsAsync(subCateogory.Id);

            return productModels;
        }
    }
}
