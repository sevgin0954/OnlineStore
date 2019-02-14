using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin
{
    public class AdminCategoriesService : BaseService, IAdminCategoriesService
    {
        public AdminCategoriesService(OnlineStoreDbContext dbContext)
            : base(dbContext) { }

        public IList<CategoryViewModel> GetAllCategories()
        {
            var dbCategories = this.DbContext.Categories
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.Products)
                .ToList();

            var models = new List<CategoryViewModel>();
            if (dbCategories.Count == 0)
            {
                return models;
            }

            foreach (var dbCategory in dbCategories)
            {
                var model = new CategoryViewModel()
                {
                    CategoryId = dbCategory.Id,
                    Name = dbCategory.Name,
                    SubCategories = dbCategory.SubCategories,
                    TotalProductsCount = dbCategory.SubCategories.Sum(sc => sc.Products.Count)
                };

                models.Add(model);
            }

            return models;
        }

        public async Task CreateCategoryAsync(CategoryBindingModel model)
        {
            var dbModel = new Category()
            {
                Name = model.Name
            };

            await this.DbContext.Categories.AddAsync(dbModel);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<SubCategoryBindingCategory> PrepareModelForAdding(string categoryId)
        {
            var dbCategory = await this.DbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (dbCategory == null)
            {
                return null;
            }

            var model = new SubCategoryBindingCategory()
            {
                CategoryId = categoryId, 
                CategoryName = dbCategory.Name
            };

            return model;
        }

        public async Task AddSubcategory(SubCategoryBindingCategory model)
        {
            var dbModel = new SubCategory()
            {
                Name = model.Name,
                CategoryId = model.CategoryId
            };

            this.DbContext.SubCategories.Add(dbModel);
            await this.DbContext.SaveChangesAsync();
        }

        private long CountTotalProducts(CategoryViewModel model)
        {
            return model.SubCategories.LongCount();
        }
    }
}
