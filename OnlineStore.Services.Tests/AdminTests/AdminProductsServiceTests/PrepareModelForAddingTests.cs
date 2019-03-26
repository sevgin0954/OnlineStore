using OnlineStore.Models;
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
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory();
            dbContext.SubCategories.Add(dbSubCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbSubCategoryId = dbSubCategory.Id;
            var model = service.PrepareModelForAdding(dbSubCategoryId);
            var modelSubCategoryId = model.SubCategoryId;

            Assert.Equal(dbSubCategoryId, modelSubCategoryId);
        }

        [Theory]
        [InlineData("SubCategoryName")]
        public void WithCorrectSubCategoryId_ShouldReturnModelWithCorrectSubCategoryName(string subCategoryName)
        {
            var dbContext = this.GetDbContext();
            var dbSubCategory = new SubCategory()
            {
                Name = subCategoryName
            };
            dbContext.SubCategories.Add(dbSubCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var dbSubCategoryId = dbSubCategory.Id;
            var model = service.PrepareModelForAdding(dbSubCategoryId);
            var modelSubCategoryName = model.SubCategoryName;

            Assert.Equal(subCategoryName, modelSubCategoryName);
        }
    }
}
