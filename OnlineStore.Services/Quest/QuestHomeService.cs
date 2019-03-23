using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using OnlineStore.Services.Quest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest
{
    public class QuestHomeService : BaseService, IQuestHomeService
    {
        private readonly IMapper mapper;

        public QuestHomeService(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

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

        public async Task<IEnumerable<ProductConciseViewModel>> GetProductsBySubcategoryAsync(string subcategoryId)
        {
            var products = await GetProductFromDatabase(subcategoryId);

            if (products == null)
            {
                return null;
            }

            var models = this.MapProductModels(products);

            return models;
        }

        public IEnumerable<ProductConciseViewModel> GetProductsByKeywords(string words)
        {
            var keyWords = ExtractKeyWords(words);
            var products = GetProductsFromDatabase();
            var filteredProducts = FilterProductsByKeyWords(keyWords, products);

            var models = this.MapProductModels(filteredProducts);

            return models;
        }

        private IEnumerable<Product> GetProductsFromDatabase()
        {
            return this.DbContext
                .Products
                .Include(p => p.SubCategory)
                .Include(p => p.Photos)
                .ToList();
        }

        private static IEnumerable<string> ExtractKeyWords(string words, int minLengthPerWord = 2)
        {
            return words
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > minLengthPerWord)
                .Select(w => w.ToLower())
                .ToList();
        }

        private IList<ProductConciseViewModel> MapProductModels(IList<Product> source)
        {
            var destination = this.mapper.Map<List<ProductConciseViewModel>>(source);

            for (int a = 0; a < destination.Count; a++)
            {
                destination[a].MainPhoto = source[a].Photos.First().Data;
                destination[a].ReviewsCount = source[a].Reviews.Count;
                destination[a].ReviewsAvgStartRating = source[a].Reviews.Count > 0 ?
                    (int)Math.Round(source[a].Reviews.Average(r => r.StarsCount), MidpointRounding.AwayFromZero)
                        :
                    0;
            }

            return destination;
        }

        private async Task<IList<Product>> GetProductFromDatabase(string subcategoryId)
        {
            var subcategory = await this.DbContext.SubCategories
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Reviews)
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(sc => sc.Id == subcategoryId);

            if (subcategory == null)
            {
                return null;
            }

            var products = subcategory.Products.ToList();

            return products;
        }

        private static IList<Product> FilterProductsByKeyWords(IEnumerable<string> keyWords, IEnumerable<Product> products)
        {
            var filteredProducts = new List<Product>();

            foreach (var product in products)
            {
                var productName = product.Name.ToLower();
                var productCategoryName = product.SubCategory.Name.ToLower();

                bool isMatch = false;

                foreach (var keyWord in keyWords)
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

            return filteredProducts;
        }
    }
}
