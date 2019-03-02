using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Common.Constants;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.OrderModels.BindingModels;
using OnlineStore.Models.WebModels.Session;
using OnlineStore.Services.Quest.Interfaces;
using OnlineStore.Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Services.UserServices
{
    public class UserOrderService : BaseService, IUserOrderService
    {
        private readonly IMapper mapper;
        public readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<User> userManager;

        public UserOrderService(
            OnlineStoreDbContext dbContext, 
            IMapper mapper,
            IShoppingCartService shoppingCartService,
            UserManager<User> userManager)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<DeliveryInfoBindingModel>> PrepareModelForChoosingAddressAsync(
            ISession session, 
            ClaimsPrincipal user)
        {
            var productsModels = this.shoppingCartService.GetProductsFromCart(session);

            if (productsModels.Count == 0)
            {
                return null;
            }

            var dbDeliveryInfos = await GetDeliveryInfosFromDatabaseAsync(user);

            var models = this.mapper.Map<IEnumerable<DeliveryInfoBindingModel>>(dbDeliveryInfos);

            return models;
        }

        public async Task<OrderBidningModel> PrepareModelForOrdering(ISession session, string deliveryInfoId)
        {
            var productsModels = this.shoppingCartService.GetProductsFromCart(session);

            if (productsModels.Count == 0)
            {
                return null;
            }

            var dbDeliveryInfo = await GetDeliveryInfoFromDatabaseAsync(deliveryInfoId);

            if (dbDeliveryInfo == null)
            {
                return null;
            }

            var model = await MapOrderAsync(productsModels, dbDeliveryInfo);

            return model;
        }

        public async Task<bool> CreateOrder(OrderBidningModel model, ClaimsPrincipal user, ISession session)
        {
            var isModelValid = await this.ValidateModel(model);

            if (isModelValid == false)
            {
                return false;
            }

            await this.RemoveProductsFromDatabase(model.Products);
            this.RemoveProductsFromSessionCart(session);

            var orderStatus = await this.GetDefaultOrderStatusFromDatabaseAsync();
            var dbOrder = this.MapOrder(model, user, orderStatus);

            await this.DbContext.Orders.AddAsync(dbOrder);

            var dbOrderProducts = this.MapOrderOrderProducts(dbOrder.Id, model.Products);
            await this.DbContext.OrderProducts.AddRangeAsync(dbOrderProducts);

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        private async Task<ICollection<DeliveryInfo>> GetDeliveryInfosFromDatabaseAsync(ClaimsPrincipal user)
        {
            var dbUser = await this.DbContext.Users
                .Where(u => u.UserName == user.Identity.Name)
                .Include(u => u.DeliveryInfos)
                    .ThenInclude(di => di.District)
                .Include(u => u.DeliveryInfos)
                    .ThenInclude(di => di.PopulatedPlace)
                .FirstOrDefaultAsync();

            return dbUser.DeliveryInfos;
        }

        private async Task<DeliveryInfo> GetDeliveryInfoFromDatabaseAsync(string deliveryInfoId)
        {
            var dbDeliveryInfo = await this.DbContext.DeliverysInfos
                   .Include(di => di.District)
                   .Include(di => di.PopulatedPlace)
                   .FirstOrDefaultAsync(di => di.Id == deliveryInfoId);

            return dbDeliveryInfo;
        }

        private async Task<OrderBidningModel> MapOrderAsync(IEnumerable<ProductSessionModel> products, DeliveryInfo deliveryInfo)
        {
            var model = new OrderBidningModel();

            await this.MapProductsAsync(model, products);

            model.DeliveryInfo = this.mapper.Map<DeliveryInfoBindingModel>(deliveryInfo);

            this.MapPaymentTypes(model);

            return model;
        }

        private async Task MapProductsAsync(OrderBidningModel model, IEnumerable<ProductSessionModel> products)
        {
            var productsModels = new List<ProductConciseBindingModel>();

            foreach (var sessionProduct in products)
            {
                var productModel = await this.MapProductAsync(sessionProduct);

                if (productModel == null)
                {
                    continue;
                }

                productsModels.Add(productModel);
            }

            model.Products = productsModels;
        }

        private async Task<ProductConciseBindingModel> MapProductAsync(ProductSessionModel sessionProduct)
        {
            var dbProduct = await this.DbContext.Products
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(p => p.Id == sessionProduct.ProductId);

            if (dbProduct == null)
            {
                return null;
            }

            var productModel = this.mapper.Map<ProductConciseBindingModel>(dbProduct);
            productModel.MainPhoto = dbProduct.Photos.FirstOrDefault();
            productModel.Count = sessionProduct.Count;

            if (dbProduct.PromoPrice != null)
            {
                productModel.Price = (decimal)dbProduct.PromoPrice;
            }

            return productModel;
        }

        private void MapPaymentTypes(OrderBidningModel model)
        {
            var dbPaymentTypes = this.DbContext.PaymentTypes
                .ToList();

            var defaultPaymentType = new SelectListItem()
            {
                Text = ControllerConstats.SelectListPlaceholderPaymentType,
                Disabled = true,
                Selected = true
            };

            model.PaymentTypes.Add(defaultPaymentType);

            foreach (var paymentType in dbPaymentTypes)
            {
                var selectItem = new SelectListItem()
                {
                    Text = paymentType.Name,
                    Value = paymentType.Id
                };

                model.PaymentTypes.Add(selectItem);
            }
        }

        private async Task<bool> ValidateModel(OrderBidningModel model)
        {
            var dbDeliveryInfo = await this.DbContext.DeliverysInfos
                .FindAsync(model.DeliveryInfo.Id);

            if (dbDeliveryInfo == null)
            {
                return false;
            }

            var paymentType = await this.DbContext.PaymentTypes
                .FindAsync(model.SelectedPaymentTypeId);

            if (paymentType == null)
            {
                return false;
            }

            var result = await this.ValidateProducts(model.Products);

            if (result == false)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateProducts(IList<ProductConciseBindingModel> products)
        {
            foreach (var productModel in products)
            {
                var dbProduct = await this.DbContext.Products
                    .FindAsync(productModel.ProductId);

                if (dbProduct == null)
                {
                    return false;
                }

                var productPrice = dbProduct.Price;

                if (dbProduct.PromoPrice != null)
                {
                    productPrice = (decimal)dbProduct.PromoPrice;
                }

                if (productModel.Price != productPrice)
                {
                    return false;
                }

                if (dbProduct.CountsLeft < productModel.Count)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task RemoveProductsFromDatabase(IList<ProductConciseBindingModel> products)
        {
            foreach (var productModel in products)
            {
                var dbProduct = await this.DbContext.Products
                    .FindAsync(productModel.ProductId);

                dbProduct.CountsLeft -= productModel.Count;
            }

            await this.DbContext.SaveChangesAsync();
        }

        private void RemoveProductsFromSessionCart(ISession session)
        {
            session.Remove(WebConstants.SessionProductsKey);
        }

        private async Task<OrderStatus> GetDefaultOrderStatusFromDatabaseAsync()
        {
            var dbOrderStatus = await this.DbContext.OrdersStatuses
                .Where(os => os.Name == WebConstants.InitialOrderStatus)
                .FirstAsync();

            return dbOrderStatus;
        }

        private Order MapOrder(OrderBidningModel model, ClaimsPrincipal user, OrderStatus orderStatus)
        {
            var dbOrder = new Order()
            {
                OrderDate = DateTime.UtcNow,
                TotalPrice = this.CalculateTotalPrice(model.Products),
                DeliveryExpectedTime = DateTime.UtcNow.AddDays(WebConstants.DeliveryRequiredDays),
                DeliveryPrice = WebConstants.DeliveryPrice,
                DeliveryInfoId = model.DeliveryInfo.Id,
                OrderStatusId = orderStatus.Id,
                PaymentTypeId = model.SelectedPaymentTypeId,
                UserId = this.userManager.GetUserId(user)
            };

            return dbOrder;
        }

        private decimal CalculateTotalPrice(IEnumerable<ProductConciseBindingModel> products)
        {
            decimal totalPrice = 0;

            foreach (var product in products)
            {
                totalPrice = product.Price * product.Count;
            }

            return totalPrice;
        }

        private IEnumerable<OrderProduct> MapOrderOrderProducts(string dbOrderId,
            IEnumerable<ProductConciseBindingModel> products)
        {
            var dbOrderProducts = new List<OrderProduct>();

            foreach (var product in products)
            {
                var dbOrderProduct = new OrderProduct()
                {
                    OrderId = dbOrderId,
                    ProductId = product.ProductId,
                    Count = product.Count
                };

                dbOrderProducts.Add(dbOrderProduct);
            }

            return dbOrderProducts;
        }
    }
}
