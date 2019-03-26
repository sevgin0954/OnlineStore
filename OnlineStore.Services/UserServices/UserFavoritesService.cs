using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.ProductModels.ViewModels;
using OnlineStore.Services.UserServices.Interfaces;

namespace OnlineStore.Services.UserServices
{
    public class UserFavoritesService : BaseService, IUserFavoritesService
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UserFavoritesService(OnlineStoreDbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<FavoriteProductViewModel>> GetAllFavoriteProducts(ClaimsPrincipal user)
        {
            var dbFavoriteProducts = await this.GetUserFavoriteProductsFromDatabaseAsync(user);
            var dbProducts = await this.GetProductsWithPhotosAndReviewsFromDatabaseAsync(dbFavoriteProducts);
            var models = this.MapFavoriteProductModels(dbProducts);

            return models;
        }

        public async Task<bool> AddProductAsync(string productId, ClaimsPrincipal user)
        {
            var dbProduct = await this.GetProductFromDatabaseAsync(productId);
            if (dbProduct == null)
            {
                return false;
            }

            var dbFavoriteProducts = await this.GetUserFavoriteProductsFromDatabaseAsync(user);

            var isProductExist = this.IsProductExistInFavoriteProducts(productId, dbFavoriteProducts);
            if (isProductExist)
            {
                return false;
            }

            await this.AddProductToFavoriteAsync(productId, dbFavoriteProducts);

            return true;
        }

        public async Task<bool> RemoveProductAsync(string productId, ClaimsPrincipal user)
        {
            var dbFavoriteProducts = await this.GetUserFavoriteProductsFromDatabaseAsync(user);

            var isProductExist = this.IsProductExistInFavoriteProducts(productId, dbFavoriteProducts);
            if (isProductExist == false)
            {
                return false;
            }

            await this.RemoveProductFromFavoriteProductsAsync(productId, dbFavoriteProducts);

            return true;
        }

        private async Task<Product> GetProductFromDatabaseAsync(string id)
        {
            var dbProduct = await this.DbContext.Products
                .FindAsync(id);

            return dbProduct;
        }

        private async Task<ICollection<UserFavoriteProduct>> GetUserFavoriteProductsFromDatabaseAsync(ClaimsPrincipal user)
        {
            var dbUser = await this.GetUserWithFavoriteFromDatabaseAsync(user);
            var dbFavoriteProducts = dbUser.UserFavoriteProducts;

            return dbFavoriteProducts;
        }

        private async Task<IEnumerable<Product>> GetProductsWithPhotosAndReviewsFromDatabaseAsync(
            ICollection<UserFavoriteProduct> dbFavoriteProducts)
        {
            var dbProducts = await this.DbContext.Products
                .Where(p => dbFavoriteProducts.Any(fp => fp.ProductId == p.Id))
                .Include(p => p.Photos)
                .Include(p => p.Reviews)
                .ToListAsync();

            return dbProducts;
        }

        private IEnumerable<FavoriteProductViewModel> MapFavoriteProductModels(IEnumerable<Product> dbProducts)
        {
            var models = new List<FavoriteProductViewModel>();

            foreach (var dbProduct in dbProducts)
            {
                var model = this.mapper.Map<FavoriteProductViewModel>(dbProduct);
                model.MainPhoto = dbProduct.Photos.First().Data;

                if (dbProduct.PromoPrice != null)
                {
                    model.Price = (decimal)dbProduct.PromoPrice;
                }

                models.Add(model);
            }

            return models;
        }

        private async Task<User> GetUserWithFavoriteFromDatabaseAsync(ClaimsPrincipal user)
        {
            var dbUser = await this.DbContext.Users
                .Where(u => u.UserName == user.Identity.Name)
                .Include(u => u.UserFavoriteProducts)
                .FirstAsync();

            return dbUser;
        }

        private bool IsProductExistInFavoriteProducts(
            string productId, 
            ICollection<UserFavoriteProduct> dbFavoriteProducts)
        {
            foreach (var favoriteProduct in dbFavoriteProducts)
            {
                if (favoriteProduct.ProductId == productId)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task AddProductToFavoriteAsync(
            string productId,
            ICollection<UserFavoriteProduct> dbFavoriteProducts)
        {
            var favoriteProductModel = new UserFavoriteProduct()
            {
                ProductId = productId
            };

            dbFavoriteProducts.Add(favoriteProductModel);

            await this.DbContext.SaveChangesAsync();
        }

        private async Task RemoveProductFromFavoriteProductsAsync(
            string productId, 
            ICollection<UserFavoriteProduct> dbFavoriteProducts)
        {
            var favoriteProductModel = dbFavoriteProducts
                .Where(fp => fp.ProductId == productId)
                .First();

            dbFavoriteProducts.Remove(favoriteProductModel);

            await this.DbContext.SaveChangesAsync();
        }
    }
}
