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

                var categoriesCount = service.GetAllCategories().Count;

                Assert.Equal(1, categoriesCount);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnZeroCategoriesCount()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnZeroCategoriesCount");
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
                var category = new Category();
                category.Name = categoryName;
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);

                var modelCategoryName = category.Name;

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
                var category = new Category();
                var product = new Product();
                var product2 = new Product();
                var subCategory = new SubCategory();

                subCategory.Products.Add(product);
                subCategory.Products.Add(product2);
                category.SubCategories.Add(subCategory);
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);
                var categories = service.GetAllCategories();

                var productCount = categories[0].TotalProductsCount;

                Assert.Equal(2, productCount);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnCorrectSubCategoriesCount()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnCorrectSubCategoriesCount");
            var mapper = this.InitializeAutoMapper();

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var dbCategory = new Category();
                var subCategory1 = new SubCategory();
                var subCategory2 = new SubCategory();

                dbCategory.SubCategories.Add(subCategory1);
                dbCategory.SubCategories.Add(subCategory2);

                dbContext.Categories.Add(dbCategory);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);

                var modelCategory = service.GetAllCategories().First();
                var subcategoriesCount = modelCategory.SubCategories.Count;

                Assert.Equal(2, subcategoriesCount);
            }
        }

        [Fact]
        public void GetAllCategories_ShouldReturnCorrectSubCategoryId()
        {
            var dbContextOptions = this.InitializeDbContextOptions("GetAllCategories_ShouldReturnCorrectSubCategoryId");
            var mapper = this.InitializeAutoMapper();

            using (var dbContext = new OnlineStoreDbContext(dbContextOptions))
            {
                var dbCategory = new Category();
                var dbSubCategory = new SubCategory();

                dbCategory.SubCategories.Add(dbSubCategory);
                dbContext.Categories.Add(dbCategory);
                dbContext.SaveChanges();

                var service = new AdminCategoriesService(dbContext, mapper);
                var modelCategory = service.GetAllCategories().First();
                var modelSubCategoryId = modelCategory.SubCategories.First().Id;

                Assert.Equal(dbSubCategory.Id, modelSubCategoryId);
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
