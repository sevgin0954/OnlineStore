using AutoMapper;
using OnlineStore.Models;
using OnlineStore.Services.Admin;
using OnlineStore.Web.Mapping;
using System.Linq;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class GetAllCategoriesTests : BaseAdminCategoriesSeviceTest
    {
        [Fact]
        public void WithOneCategory_ShouldReturnCorrectCategoriesCount()
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category();
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var categoriesCount = service.GetAllCategories().Count;

            Assert.Equal(1, categoriesCount);
        }

        [Fact]
        public void WithoutCategories_ShouldReturnZeroCategoriesCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var categories = service.GetAllCategories();

            Assert.Equal(0, categories.Count);
        }

        [Fact]
        public void WithOneCategoryWithId_ShouldReturnCorrectCategoryId()
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category();
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbCategoryId = dbCategory.Id;
            var categoryModel = service.GetAllCategories().First();

            Assert.Equal(dbCategoryId, categoryModel.CategoryId);
        }

        [Theory]
        [InlineData("Category")]
        public void WithOneCategoryWithName_ShouldReturnCorrectCategoryName(string categoryName)
        {
            var dbContext = this.GetDbContext();
            var category = this.CreateCategory(categoryName);
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var modelCategoryName = category.Name;

            Assert.Equal(categoryName, modelCategoryName);
        }

        [Fact]
        public void WithoutProducts_ShouldReturnZeroTotalProductsCount()
        {
            var dbContext = this.GetDbContext();
            var category = new Category();
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var categories = service.GetAllCategories();

            var productCount = categories[0].TotalProductsCount;

            Assert.Equal(0, productCount);
        }

        [Fact]
        public void WithTwoProducts_ShouldReturnCorrectTotalProductsCount()
        {
            var dbContext = this.GetDbContext();
            var category = new Category();
            var product = new Product();
            var product2 = new Product();
            var subCategory = new SubCategory();

            subCategory.Products.Add(product);
            subCategory.Products.Add(product2);
            category.SubCategories.Add(subCategory);
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var categories = service.GetAllCategories();

            var productCount = categories[0].TotalProductsCount;

            Assert.Equal(2, productCount);
        }

        [Fact]
        public void WithoutSubcategories_ShouldReturnZeroSubCategoriesCount()
        {
            var dbContext = this.GetDbContext();
            var category = new Category();
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var categories = service.GetAllCategories();

            var subCategory = categories[0].SubCategories;
            var subCategoriesCount = subCategory.Count;

            Assert.Equal(0, subCategoriesCount);
        }

        [Fact]
        public void WithTwoSubcategories_ShouldReturnCorrectSubCategoriesCount()
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category();
            var subCategory1 = new SubCategory();
            var subCategory2 = new SubCategory();

            dbCategory.SubCategories.Add(subCategory1);
            dbCategory.SubCategories.Add(subCategory2);

            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var modelCategory = service.GetAllCategories().First();
            var subcategoriesCount = modelCategory.SubCategories.Count;

            Assert.Equal(2, subcategoriesCount);
        }

        [Fact]
        public void WithOneSubcategoryWithId_ShouldReturnCorrectSubCategoryId()
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category();
            var dbSubCategory = new SubCategory();

            dbCategory.SubCategories.Add(dbSubCategory);
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var modelCategory = service.GetAllCategories().First();
            var modelSubCategoryId = modelCategory.SubCategories.First().Id;

            Assert.Equal(dbSubCategory.Id, modelSubCategoryId);
        }

        [Theory]
        [InlineData("SubCategory")]
        public void WithOneSubcategoryWithName_ShouldReturnCorrectSubCategoryName(string subCategoryName)
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category();
            var dbSubCategory = this.CreateSubcategory(subCategoryName);
            dbCategory.SubCategories.Add(dbSubCategory);
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var categoryModel = service.GetAllCategories().First();
            var subCategoryModel = categoryModel.SubCategories.First();

            Assert.Equal(subCategoryName, subCategoryModel.Name);
        }

        private Product CreateProduct(string name)
        {
            var product = new Product();
            product.Name = "Product";

            return product;
        }

        private Category CreateCategory(string name)
        {
            var category = new Category();
            category.Name = name;

            return category;
        }

        private SubCategory CreateSubcategory(string name)
        {
            var subCategory = new SubCategory();
            subCategory.Name = name;

            return subCategory;
        }
    }
}
