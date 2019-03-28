using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminProductsServiceTests
{
    public class PrepareModelForAddingTests : BaseAdminProductsServiceTest
    {
        [Fact]
        public void WithIncorrectSubCategoryId_ShouldReturnNull()
        {
            const string subcategoryId = "1";

            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = service.PrepareModelForAdding(subcategoryId);

            Assert.Null(model);
        }

        [Fact]
        public void WithCorrectSubCategoryId_ShouldReturnModelWithCorrectSubCategoryId()
        {
            var dbSubCategory = new SubCategory();

            var model = this.CallPrepareModelForAdding(dbSubCategory);
            var dbSubCategoryId = dbSubCategory.Id;
            var modelSubCategoryId = model.SubCategoryId;

            Assert.Equal(dbSubCategoryId, modelSubCategoryId);
        }

        [Theory]
        [InlineData("SubCategoryName")]
        public void WithCorrectSubCategoryId_ShouldReturnModelWithCorrectSubCategoryName(string subCategoryName)
        {
            var dbSubCategory = new SubCategory()
            {
                Name = subCategoryName
            };

            var model = this.CallPrepareModelForAdding(dbSubCategory);
            var dbSubCategoryId = dbSubCategory.Id;
            var modelSubCategoryName = model.SubCategoryName;

            Assert.Equal(subCategoryName, modelSubCategoryName);
        }

        private ProductBindingModel CallPrepareModelForAdding(SubCategory subCategory)
        {
            var dbContext = this.GetDbContext();
            dbContext.SubCategories.Add(subCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbSubCategoryId = subCategory.Id;
            var model = service.PrepareModelForAdding(dbSubCategoryId);

            return model;
        }
    }
}
