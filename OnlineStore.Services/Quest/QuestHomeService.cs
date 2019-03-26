using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using OnlineStore.Services.Quest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest
{
    public class QuestHomeService : BaseService, IQuestHomeService
    {
        private readonly IMapper mapper;
        private readonly SignInManager<User> signInManager;

        public QuestHomeService(OnlineStoreDbContext dbContext, IMapper mapper, SignInManager<User> signInManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.signInManager = signInManager;
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

        public async Task<IEnumerable<ProductConciseViewModel>> GetProductsBySubcategoryAsync(
            string subcategoryId, 
            ClaimsPrincipal user)
        {
            var products = await GetProductFromDatabase(subcategoryId);

            if (products == null)
            {
                return null;
            }

            var dbUserFavoriteProducts = this.GetUserFavoriteProducts(user);

            var models = this.MapProductModels(products, dbUserFavoriteProducts);

            return models;
        }

        public IEnumerable<ProductConciseViewModel> GetProductsByKeywords(string words, ClaimsPrincipal user)
        {
            var keyWords = ExtractKeyWords(words);
            var products = GetProductsFromDatabase();
            var filteredProducts = FilterProductsByKeyWords(keyWords, products);

            var dbUserFavoriteProducts = this.GetUserFavoriteProducts(user);
            var models = this.MapProductModels(filteredProducts, dbUserFavoriteProducts);

            return models;
        }

        private ICollection<UserFavoriteProduct> GetUserFavoriteProducts(ClaimsPrincipal user)
        {
            var dbUser = GetUserWithFavoriteProductsFromDatabase(user);
            if (dbUser == null)
            {
                return new List<UserFavoriteProduct>();
            }
            var dbUserFavoriteProducts = dbUser.UserFavoriteProducts;

            return dbUserFavoriteProducts;
        }

        private User GetUserWithFavoriteProductsFromDatabase(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated == false)
            {
                return null;
            }

            var dbUser = this.DbContext.Users
                .Where(u => u.UserName == user.Identity.Name)
                .Include(u => u.UserFavoriteProducts)
                .First();

            return dbUser;
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

        private IList<ProductConciseViewModel> MapProductModels(
            IList<Product> source, 
            IEnumerable<UserFavoriteProduct> userFavoriteProducts)
        {
            var productModel = this.mapper.Map<List<ProductConciseViewModel>>(source);

            for (int a = 0; a < productModel.Count; a++)
            {
                productModel[a].MainPhoto = source[a].Photos.First().Data;
                productModel[a].ReviewsCount = source[a].Reviews.Count;
                productModel[a].ReviewsAvgStartRating = source[a].Reviews.Count > 0 ?
                    (int)Math.Round(source[a].Reviews.Average(r => r.StarsCount), MidpointRounding.AwayFromZero)
                        :
                    0;

                var currentProductId = productModel[a].Id;
                var isProductExist = this.IsExistInFavorites(currentProductId, userFavoriteProducts);
                productModel[a].IsAddedToFavorite = isProductExist;
            }

            return productModel;
        }

        private bool IsExistInFavorites(string productId, IEnumerable<UserFavoriteProduct> userFavoriteProducts)
        {
            foreach (var userProduct in userFavoriteProducts)
            {
                if (userProduct.ProductId == productId)
                {
                    return true;
                }
            }

            return false;
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
