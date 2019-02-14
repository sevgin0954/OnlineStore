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

            for (int a = 0; a < models.Count; a++)
            {
                models[a].MainPhoto = products[a].Photos.First().Data;
                models[a].ReviewsCount = products[a].Reviews.Count;
                models[a].ReviewsAvgStartRating = products[a].Reviews.Count > 0 ?
                    (int)Math.Round(products[a].Reviews.Average(r => r.StarsCount), MidpointRounding.AwayFromZero)
                        : 
                    0;
            }

            return models;
        }
    }
}
