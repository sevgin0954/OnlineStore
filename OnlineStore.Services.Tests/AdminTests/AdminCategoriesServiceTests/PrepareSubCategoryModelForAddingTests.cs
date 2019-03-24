using OnlineStore.Models;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class PrepareSubCategoryModelForAddingTests : BaseAdminCategoriesSeviceTest
    {
        [Fact]
        public async Task WithCorrectCategoryId_ShouldReturnModelWithCorrectCategoryId()
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category();
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();
            var service = this.GetService(dbContext);

            var dbCategoryId = dbCategory.Id;
            var model = await service.PrepareSubCategoryModelForAdding(dbCategoryId);

            Assert.Equal(dbCategoryId, model.CategoryId);
        }

        [Theory]
        [InlineData("categoryName")]
        public async Task WithCorrectCategoryId_ShouldReturnModelWithCorrectCategoryName(string categoryName)
        {
            var dbContext = this.GetDbContext();
            var dbCategory = new Category
            {
                Name = categoryName
            };
            dbContext.Categories.Add(dbCategory);
            dbContext.SaveChanges();
            var service = this.GetService(dbContext);
            
            var model = await service.PrepareSubCategoryModelForAdding(dbCategory.Id);

            Assert.Equal(categoryName, model.CategoryName);
        }

        [Theory]
        [InlineData("1")]
        public async Task WithIncorrectCategoryId_ShouldReturnNull(string categoryId)
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.PrepareSubCategoryModelForAdding(categoryId);

            Assert.Null(model);
        }
    }
}
