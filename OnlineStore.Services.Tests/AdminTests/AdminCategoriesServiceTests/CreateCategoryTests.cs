using OnlineStore.Models.WebModels.Admin.BindingModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class CreateCategoryTests : BaseAdminCategoriesSeviceTest
    {
        [Theory]
        [InlineData("categoryName")]
        public async Task WithModelWithName_ShouldCreateCategoryWithCorrectName(string categoryName)
        {
            var categoryModel = new CategoryBindingModel();
            categoryModel.Name = categoryName;
            var dbContext = this.GetDbContext();

            var service = this.GetService(dbContext);

            await service.CreateCategoryAsync(categoryModel);
            var dbCategory = dbContext.Categories.First();

            Assert.Equal(categoryName, dbCategory.Name);
        }
    }
}
