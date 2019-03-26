using OnlineStore.Models.WebModels.Admin.BindingModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class CreateCategoryTests : BaseAdminCategoriesSeviceTest
    {
        [Theory]
        [InlineData("categoryName")]
        public async Task WithModelWithName_ShouldCreateCategoryWithSameName(string categoryName)
        {
            var categoryModel = new CategoryBindingModel
            {
                Name = categoryName
            };
            var dbContext = this.GetDbContext();

            var service = this.GetService(dbContext);

            await service.CreateCategoryAsync(categoryModel);
            var dbCategory = dbContext.Categories.First();

            Assert.Equal(categoryName, dbCategory.Name);
        }

        [Fact]
        public void WithModelNull_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();

            var service = this.GetService(dbContext);

            Func<Task> func = async () => await service.CreateCategoryAsync(null);

            Assert.ThrowsAsync<NullReferenceException>(func);
        }
    }
}
