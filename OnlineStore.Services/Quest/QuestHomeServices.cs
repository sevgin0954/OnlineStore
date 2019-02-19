using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using OnlineStore.Services.Quest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest
{
    public class QuestHomeServices : BaseService, IQuestHomeServices
    {
        public QuestHomeServices(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public QuestHomeServices(OnlineStoreDbContext dbContext)
            : base(dbContext) { }

        public IndexViewModel PrepareIndexModel()
        {
            var dbCategories = this.DbContext.Categories
                .Include(c => c.SubCategories)
                .ToList();

            var model = new IndexViewModel()
            {
                Categories = dbCategories
            };

            return model;
        }

        public async Task<IEnumerable<ProductConciseViewModel>> GetProductsAsync(string subcategoryId)
        {
            var subcategory = await this.DbContext.SubCategories
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Reviews)
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(sc => sc.Id == subcategoryId);

            var products = subcategory.Products.ToList();

            if (subcategory == null)
            {
                return null;
            }

            var models = this.Mapper.Map<List<ProductConciseViewModel>>(products);

            this.MapProductModel(products, models);

            return models;
        }

        public IEnumerable<ProductConciseViewModel> GetProductsByKeywordsAsync(string words)
        {
            var wordsSplit = words
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > 3)
                .Select(w => w.ToLower())
                .ToList();

            var products = this.DbContext
                .Products
                .Include(p => p.SubCategory)
                .Include(p => p.Photos)
                .ToList();

            var filteredProducts = new List<Models.Product>();

            foreach (var product in products)
            {
                var productName = product.Name.ToLower();
                var productCategoryName = product.SubCategory.Name.ToLower();

                bool isMatch = false;

                foreach (var keyWord in wordsSplit)
                {
                    if (productName.IndexOf(keyWord) >= 0)
                    {
                        isMatch = true;
                        break;
                    }

                    if (productCategoryName.IndexOf(keyWord) >= 0)
                    {
                        isMatch = true;
                        break;
                    }
                }

                if (isMatch)
                {
                    filteredProducts.Add(product);
                }
            }

            var models = this.Mapper.Map<List<ProductConciseViewModel>>(filteredProducts);

            this.MapProductModel(filteredProducts, models);

            return models;
        }

        private void MapProductModel(List<Models.Product> source, List<ProductConciseViewModel> destination)
        {
            for (int a = 0; a < destination.Count; a++)
            {
                destination[a].MainPhoto = source[a].Photos.First().Data;
                destination[a].ReviewsCount = source[a].Reviews.Count;
                destination[a].ReviewsAvgStartRating = source[a].Reviews.Count > 0 ?
                    (int)Math.Round(source[a].Reviews.Average(r => r.StarsCount), MidpointRounding.AwayFromZero)
                        :
                    0;
            }
        }
    }
}
