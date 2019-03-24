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
            var model = new SubCategoryBindingCategory()
            {
                CategoryId = categoryId
            };
            var service = this.GetService(dbContext);

            await service.AddSubcategory(model);
            var dbSubCategory = dbContext.SubCategories.First();

            Assert.Equal(categoryId, dbSubCategory.CategoryId);
        }

        [Theory]
        [InlineData("subCategoryName")]
        public async Task WithModelWithName_ShouldAddSubcategoryWithSameName(string subCategoryName)
        {
            var dbContext = this.GetDbContext();
            var model = new SubCategoryBindingCategory()
            {
                Name = subCategoryName
            };
            var service = this.GetService(dbContext);

            await service.AddSubcategory(model);
            var dbSubCategory = dbContext.SubCategories.First();

            Assert.Equal(subCategoryName, dbSubCategory.Name);
        }
    }
}
