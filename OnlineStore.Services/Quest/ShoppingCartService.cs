using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Common.Helpers;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Quest.BindingModels;
using OnlineStore.Models.WebModels.Quest.ViewModels;
using OnlineStore.Models.WebModels.Session;
using OnlineStore.Services.Quest.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services.Quest
{
    public class ShoppingCartService : BaseService, IShoppingCartService
    {
        private readonly IMapper mapper;

        public ShoppingCartService(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductShoppingCartViewModel>> GetProductsAsync(ISession session)
        {
            var productSessionModels = this.GetProductFromCart(session);

            if (productSessionModels == null)
            {
                return null;
            }

            var productsModels = await MapProductModels(productSessionModels);

            return productsModels;
        }

        public async Task AddProductAsync(string productId, ISession session)
        {
            var dbProduct = await DbContext.Products
                .FindAsync(productId);

            if (dbProduct != null)
            {
                AddProductToSession(dbProduct, session);
            }
        }

        public async Task UpdateProductCountAsync(ProductCardBindingModel model, ISession session)
        {
            var dbProduct = await DbContext.Products
                .FindAsync(model.ProductId);

            if (dbProduct != null)
            {
                UpdateProductCountFromSession(model, session);
            }
        }

        public async Task<bool> RemoveProduct(string productId, ISession session)
        {
            var dbProduct = await DbContext.Products
                .FindAsync(productId);

            if (dbProduct == null)
            {
                return false;
            }

            var prodcutsInCart = GetProductFromCart(session);
            var productIndex = FindProductByIndex(productId, prodcutsInCart);

            if (productIndex < 0)
            {
                return false;
            }

            prodcutsInCart.RemoveAt(productIndex);

            UpdateSession(session, prodcutsInCart);

            return true;
        }

        private List<ProductSessionModel> GetProductFromCart(ISession session)
        {
            var prodcutsInCart =
                    session.GetObjectFromJson<List<ProductSessionModel>>(WebConstants.SessionProductsKey);


            if (prodcutsInCart == null)
            {
                prodcutsInCart = new List<ProductSessionModel>();
            }

            return prodcutsInCart;
        }

        private int FindProductByIndex(string productId, List<ProductSessionModel> prodcutsInCart)
        {
            return prodcutsInCart.FindIndex(p => p.ProductId == productId);
        }

        private void UpdateSession(ISession session, List<ProductSessionModel> prodcutsInCart)
        {
            session.SetObjectAsJson(WebConstants.SessionProductsKey, prodcutsInCart);
        }

        private async Task<List<ProductShoppingCartViewModel>> MapProductModels(
            List<ProductSessionModel> productSessionModels)
        {
            var productsModels = new List<ProductShoppingCartViewModel>();

            foreach (var product in productSessionModels)
            {
                var dbModel = await this.DbContext.Products
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(p => p.Id == product.ProductId);

                if (dbModel != null)
                {
                    var model = this.mapper.Map<ProductShoppingCartViewModel>(dbModel);
                    model.MainPhoto = dbModel.Photos.First();
                    model.Count = product.Count;

                    if (dbModel.PromoPrice != null)
                    {
                        model.Price = (int)dbModel.PromoPrice;
                    }

                    model.Price *= model.Count;

                    productsModels.Add(model);
                }
            }

            return productsModels;
        }

        private void AddProductToSession(Product dbProduct, ISession session)
        {
            var prodcutsInCart = GetProductFromCart(session);
            var productIdIndex = FindProductByIndex(dbProduct.Id, prodcutsInCart);

            if (productIdIndex < 0)
            {
                var productModel = this.mapper.Map<ProductSessionModel>(dbProduct);
                prodcutsInCart.Add(productModel);
                productIdIndex = prodcutsInCart.Count - 1;
            }

            prodcutsInCart[productIdIndex].Count++;

            UpdateSession(session, prodcutsInCart);
        }

        private void UpdateProductCountFromSession(ProductCardBindingModel model, ISession session)
        {
            var prodcutsInCart = GetProductFromCart(session);
            var productIndex = FindProductByIndex(model.ProductId, prodcutsInCart);

            if (productIndex < 0)
            {
                return;
            }

            prodcutsInCart[productIndex].Count = model.OrderQuantity;

            UpdateSession(session, prodcutsInCart);
        }
    }
}
