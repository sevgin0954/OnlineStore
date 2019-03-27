using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Models.WebModels.Admin.ViewModels;
using OnlineStore.Services.Admin.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OnlineStore.Services.Admin
{
    public class AdminProductsService : BaseService, IAdminProductsService
    {
        private readonly IMapper mapper;

        public AdminProductsService(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public ProductBindingModel PrepareModelForAdding(string subcategoryId)
        {
            var dbSubcategory = this.DbContext.SubCategories
                .Find(subcategoryId);

            if (dbSubcategory == null)
            {
                return null;
            }

            var model = this.mapper.Map<ProductBindingModel>(dbSubcategory);

            return model;
        }

        public async Task AddProduct(ProductBindingModel model)
        {
            var dbModel = this.mapper.Map<Product>(model);

            await this.MapPhotos(dbModel, model.Photos);

            await this.DbContext.Products.AddAsync(dbModel);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(string subcategoryId)
        {
            var subcategory = await this.DbContext.SubCategories
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.Orders)
                .Include(sc => sc.Products)
                    .ThenInclude(p => p.SubCategory)
                .FirstOrDefaultAsync(sc => sc.Id == subcategoryId);

            if (subcategory == null)
            {
                return null;
            }

            var models = this.mapper.Map<List<ProductViewModel>>(subcategory.Products);

            return models;
        }

        public async Task<ProductBindingModel> PrepareModelForEditing(string productId)
        {
            var product = await this.DbContext.Products
                .Include(p => p.Photos)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return null;
            }

            var model = this.mapper.Map<ProductBindingModel>(product);

            return model;
        }

        public async Task<bool> Edit(ProductBindingModel model, string productId)
        {
            if (productId == null)
            {
                return false;
            }

            var dbModel = await this.DbContext.Products
                .FindAsync(productId);

            if (dbModel == null)
            {
                return false;
            }

            this.mapper.Map(model, dbModel);

            if (model.Photos.Count > 0)
            {
                await this.MapPhotos(dbModel, model.Photos);
            }

            await this.DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(string productId)
        {
            if (productId == null)
            {
                return false;
            }

            var product = await this.DbContext.Products
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return false;
            }

            this.DbContext.Photos.RemoveRange(product.Photos);
            this.DbContext.Products.Remove(product);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        private async Task MapPhotos(Product destination, IEnumerable<IFormFile> photos)
        {
            using (var memoryStream = new MemoryStream())
            {
                foreach (var photo in photos)
                {
                    await photo.CopyToAsync(memoryStream);
                    var dbPhoto = new Photo() { Data = memoryStream.ToArray() };
                    destination.Photos.Add(dbPhoto);
                }
            }
        }
    }
}
