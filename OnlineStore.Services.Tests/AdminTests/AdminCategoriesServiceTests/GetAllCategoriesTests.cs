using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class GetAllCategoriesTests : BaseAdminCategoriesSeviceTest
    {
        [Fact]
        public void WithOneCategory_ShouldReturnCorrectCategoriesCount()
        {
            var dbCategory = new Category();

            var categoryModels = this.CallGetAllCategories(dbCategory);
            var categoriesCount = categoryModels.Count;

            Assert.Equal(1, categoriesCount);
        }

        [Fact]
        public void WithoutCategories_ShouldReturnZeroCategoriesCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var categories = service.GetAllCategories();
            var categoriesCount = categories.Count;

            Assert.Equal(0, categoriesCount);
        }

        [Fact]
        public void WithOneCategoryWithId_ShouldReturnModelWithCorrectCategoryId()
        {
            var dbCategory = new Category();

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var dbCategoryId = dbCategory.Id;
            var firstCategoryModel = categoryModels.First();
            var modelCategoryId = firstCategoryModel.CategoryId;

            Assert.Equal(dbCategoryId, modelCategoryId);
        }

        [Theory]
        [InlineData("Category")]
        public void WithOneCategoryWithName_ShouldReturnModelWithCorrectCategoryName(string categoryName)
        {
            var dbCategory = this.CreateCategory(categoryName);

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelCategoryName = firstCategoryModel.Name;

            Assert.Equal(categoryName, modelCategoryName);
        }

        [Fact]
        public void WithoutProducts_ShouldReturnZeroTotalProductsCount()
        {
            var dbCategory = new Category();

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelProductCount = firstCategoryModel.TotalProductsCount;

            Assert.Equal(0, modelProductCount);
        }

        [Fact]
        public void WithTwoProducts_ShouldReturnCorrectTotalProductsCount()
        {
            var dbCategory = new Category();
            var dbProduct = new Product();
            var dbProduct2 = new Product();
            var dbSubCategory = new SubCategory();
            dbSubCategory.Products.Add(dbProduct);
            dbSubCategory.Products.Add(dbProduct2);
            dbCategory.SubCategories.Add(dbSubCategory);

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelProductCount = firstCategoryModel.TotalProductsCount;

            Assert.Equal(2, modelProductCount);
        }

        [Fact]
        public void WithoutSubcategories_ShouldReturnZeroSubCategoriesCount()
        {
            var dbCategory = new Category();

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelSubCategory = firstCategoryModel.SubCategories;
            var modelSubCategoriesCount = modelSubCategory.Count;

            Assert.Equal(0, modelSubCategoriesCount);
        }

        [Fact]
        public void WithTwoSubcategories_ShouldReturnCorrectSubCategoriesCount()
        {
            var dbCategory = new Category();
            var dbSubCategory1 = new SubCategory();
            var dbSubCategory2 = new SubCategory();
            dbCategory.SubCategories.Add(dbSubCategory1);
            dbCategory.SubCategories.Add(dbSubCategory2);

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelSubCategories = firstCategoryModel.SubCategories;
            var modelSubcategoriesCount = modelSubCategories.Count;

            Assert.Equal(2, modelSubcategoriesCount);
        }

        [Fact]
        public void WithOneSubcategoryWithId_ShouldReturnModelWithCorrectSubCategoryId()
        {
            var dbCategory = new Category();
            var dbSubCategory = new SubCategory();
            dbCategory.SubCategories.Add(dbSubCategory);

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelFirstSubcategory = firstCategoryModel.SubCategories.First();
            var modelFirstSubcategoryId = modelFirstSubcategory.Id;

            Assert.Equal(dbSubCategory.Id, modelFirstSubcategoryId);
        }

        [Theory]
        [InlineData("SubCategory")]
        public void WithOneSubcategoryWithName_ShouldReturnModelWithCorrectSubCategoryName(string subCategoryName)
        {
            var dbCategory = new Category();
            var dbSubCategory = this.CreateSubcategory(subCategoryName);
            dbCategory.SubCategories.Add(dbSubCategory);

            var categoryModels = this.CallGetAllCategories(dbCategory);

            var firstCategoryModel = categoryModels.First();
            var modelFirstSubCategory = firstCategoryModel.SubCategories.First();
            var modelFirstSubCategoryName = modelFirstSubCategory.Name;

            Assert.Equal(subCategoryName, modelFirstSubCategoryName);
        }

        private IList<CategoryViewModel> CallGetAllCategories(Category category)
        {
            var dbContext = this.GetDbContext();
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var categoryModels = service.GetAllCategories();

            return categoryModels;
        }

        private Product CreateProduct(string name)
        {
            var product = new Product
            {
                Name = name
            };

            return product;
        }

        private Category CreateCategory(string name)
        {
            var category = new Category
            {
                Name = name
            };

            return category;
        }

        private SubCategory CreateSubcategory(string name)
        {
            var subCategory = new SubCategory
            {
                Name = name
            };

            return subCategory;
        }
    }
}
