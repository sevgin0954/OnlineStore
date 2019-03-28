using OnlineStore.Data;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.Services.Tests.AdminTests.AdminCategoriesServiceTests
{
    public class AddSubcategoryTests : BaseAdminCategoriesSeviceTest
    {
        [Fact]
        public void WithModelNull_ShouldTrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            Func<Task> func = async () => await service.AddSubcategory(null);

            Assert.ThrowsAsync<NullReferenceException>(func);
        }

        [Theory]
        [InlineData("1")]
        public async Task WithModelWithCategoryId_ShouldAddSubcategoryWithSameCategoryId(string categoryId)
        {
            var dbContext = this.GetDbContext();
            var model = new SubCategoryCategoryBindingModel()
            {
                CategoryId = categoryId
            };

            await this.CallAddSubcategory(dbContext, model);

            var dbSubCategory = dbContext.SubCategories.First();

            Assert.Equal(categoryId, dbSubCategory.CategoryId);
        }

        [Theory]
        [InlineData("subCategoryName")]
        public async Task WithModelWithName_ShouldAddSubcategoryWithSameName(string subCategoryName)
        {
            var dbContext = this.GetDbContext();
            var model = new SubCategoryCategoryBindingModel()
            {
                Name = subCategoryName
            };

            await this.CallAddSubcategory(dbContext, model);

            var dbSubCategory = dbContext.SubCategories.First();

            Assert.Equal(subCategoryName, dbSubCategory.Name);
        }

        private async Task CallAddSubcategory(OnlineStoreDbContext dbContext, SubCategoryCategoryBindingModel model)
        {
            var service = this.GetService(dbContext);

            await service.AddSubcategory(model);
        }
    }
}
