using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using System;
using System.IO;
using System.Linq;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class AddProductTests : BaseAdminProductsServiceTest
    {
        [Fact]
        public void WithModel_ShouldAddNewProductToDatabase()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new ProductBindingModel();

            service.AddProduct(model);
            var dbProducts = dbContext.Products;
            var dbProductsCount = dbProducts.Count();

            Assert.Equal(1, dbProductsCount);
        }

        [Fact]
        public void WithModelWithSubCategoryId_ShouldAddNewProductToDatabaseWithCorrectSubCategoryId()
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            dbContext.SubCategories.Add(dbSubCategory);
            dbContext.SaveChanges();

            var model = new ProductBindingModel()
            {
                SubCategoryId = dbSubCategory.Id
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductSubCategoryId = dbFirstProduct.SubCategoryId;
            var modelSubCategoryId = model.SubCategoryId;

            Assert.Equal(modelSubCategoryId, dbProductSubCategoryId);
        }

        [Theory]
        [InlineData("ProductName")]
        public void WithModelWithProductName_ShouldAddNewProductToDatabaseWithCorrectProductName(string productName)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                ProductName = productName
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductName = dbFirstProduct.Name;

            Assert.Equal(productName, dbProductName);
        }

        [Theory]
        [InlineData(5)]
        public void WithModelWithPrice_ShouldAddNewProductToDatabaseWithCorrectPrice(decimal price)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                Price = price
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductPrice = dbFirstProduct.Price;

            Assert.Equal(price, dbProductPrice);
        }

        [Theory]
        [InlineData(5)]
        public void WithModelWithPromoPrice_ShouldAddNewProductToDatabaseWithCorrectPromoPrice(decimal promoPrice)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                PromoPrice = promoPrice
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductPromoPrice = dbFirstProduct.PromoPrice;

            Assert.Equal(promoPrice, dbProductPromoPrice);
        }

        [Fact]
        public void WithModelWithoutPromoPrice_ShouldAddNewProductToDatabaseWithNullPromoPrice()
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel();
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductPromoPrice = dbFirstProduct.PromoPrice;

            Assert.Null(dbProductPromoPrice);
        }

        [Theory]
        [InlineData(5)]
        public void WithModelWithCountsLeft_ShouldAddNewProductToDatabaseWithCorrectCountsLeft(int coutsLeft)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                CountsLeft = coutsLeft
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductCountsLeft = dbFirstProduct.CountsLeft;

            Assert.Equal(coutsLeft, dbProductCountsLeft);
        }

        [Theory]
        [InlineData("Description")]
        public void WithModelWithDescription_ShouldAddNewProductToDatabaseWithCorrectDescription(string description)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                Description = description
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductDescription = dbFirstProduct.Description;

            Assert.Equal(description, dbProductDescription);
        }

        [Theory]
        [InlineData("Specifications")]
        public void WithModelWithSpecifications_ShouldAddNewProductToDatabaseWithCorrectSpecifications(string specifications)
        {
            var dbContext = this.GetDbContext();
            var model = new ProductBindingModel()
            {
                Specifications = specifications
            };
            var service = this.GetService(dbContext);

            service.AddProduct(model);
            var dbFirstProduct = dbContext.Products.First();
            var dbProductSpecifications = dbFirstProduct.Specifications;

            Assert.Equal(specifications, dbProductSpecifications);
        }
    }
}
