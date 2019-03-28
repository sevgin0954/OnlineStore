using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class PrepareSubCategoryModelForAddingTests : BaseAdminCategoriesSeviceTest
    {
        [Theory]
        [InlineData("1")]
        public async Task WithIncorrectCategoryId_ShouldReturnNull(string categoryId)
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.PrepareSubCategoryModelForAdding(categoryId);

            Assert.Null(model);
        }

        [Fact]
        public async Task WithCorrectCategoryId_ShouldReturnModelWithCorrectCategoryId()
        {
            var dbCategory = new Category();

            var model = await this.CallPrepareSubCategoryModelForAdding(dbCategory);
            var dbCategoryId = dbCategory.Id;
            var modelCategoryId = model.CategoryId;

            Assert.Equal(dbCategoryId, modelCategoryId);
        }

        [Theory]
        [InlineData("categoryName")]
        public async Task WithCorrectCategoryId_ShouldReturnModelWithCorrectCategoryName(string categoryName)
        {
            var dbCategory = new Category
            {
                Name = categoryName
            };

            var model = await this.CallPrepareSubCategoryModelForAdding(dbCategory);
            var modelName = model.CategoryName;

            Assert.Equal(categoryName, modelName);
        }

        private async Task<SubCategoryCategoryBindingModel> CallPrepareSubCategoryModelForAdding(Category dbCategory)
        {
            var dbContext = this.GetDbContext();
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();

            var service = this.GetService(dbContext);

            var model = await service.PrepareSubCategoryModelForAdding(dbCategory.Id);

            return model;
        }
    }
}
