using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Services.Admin;
using OnlineStore.Web.Mapping;
using System.Linq;
using Xunit;

namespace OnlineStore.Tests.AdminCategoriesServiceTests
{
    public class GetAllCategoriesTests
    {
        [Fact]
        public void GetAllCategories_ShouldReturnCorrectCategoriesCount()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnCorrectCategoriesCount");
            var mapper = this.InitializeAutoMapper();

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var dbSubCategory = new SubCategory();
                var dbCategory = new Category();

                dbCategory.SubCategories.Add(dbSubCategory);
                dbContext.Categories.Add(dbCategory);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);

                var categories = service.GetAllCategories();

                Assert.Equal(1, categories.Count);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnZero()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnZero");
            var mapper = this.InitializeAutoMapper();

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var service = new AdminCategoriesService(dbContext, mapper);

                var categories = service.GetAllCategories();

                Assert.Equal(0, categories.Count);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnCorrectCategoryId()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnCorrectCategoryId");
            var mapper = this.InitializeAutoMapper();

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var dbCategory = new Category();
                dbContext.Categories.Add(dbCategory);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);

                var dbCategoryId = dbCategory.Id;
                var modelCategoryId = service.GetAllCategories().First().CategoryId;

                Assert.Equal(dbCategoryId, modelCategoryId);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnCorrectCategoryName()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnCorrectCategoryName");
            var mapper = this.InitializeAutoMapper();
            const string categoryName = "Category";

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var dbCategory = new Category();
                dbCategory.Name = categoryName;
                dbContext.Categories.Add(dbCategory);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);

                var modelCategoryName = dbCategory.Name;

                Assert.Equal(categoryName, modelCategoryName);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnCorrectTotalProductsCount()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnCorrectTotalProductsCount");
            var mapper = this.InitializeAutoMapper();

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var product = new Product();
                var product2 = new Product();
                var category = new Category();
                var subCategory = new SubCategory();

                category.SubCategories.Add(subCategory);

                subCategory.Products.Add(product);
                subCategory.Products.Add(product2);

                dbContext.Categories.Add(category);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);
                var categories = service.GetAllCategories();

                var productCount = categories[0].TotalProductsCount;

                Assert.Equal(2, productCount);
            }
        }

        private DbContextOptions<OnlineStoreDbContext> InitializeDbContextOptions(string name)
        {
            var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;

            return options;
        }

        private IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();

            return mapper;
        }

        private Product CreateProduct(string name)
        {
            var product = new Product();
            product.Name = "Product";

            return product;
        }

        private SubCategory CreateSubcategory(string name)
        {
            var subCategory = new SubCategory();
            subCategory.Name = name;

            return subCategory;
        }
    }
}
