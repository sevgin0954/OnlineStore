using AutoMapper;
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
    public class AdminProductsServices : BaseService, IAdminProductsServices
    {
        public AdminProductsServices(OnlineStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public ProductBindingModel PrepareModelForAddding(string subcategoryId)
        {
            var dbSubcategory = this.DbContext.SubCategories.Find(subcategoryId);

            if (dbSubcategory == null)
            {
                return null;
            }

            var model = new ProductBindingModel()
            {
                SubCategoryId = subcategoryId,
                SubCategoryName = dbSubcategory.Name
            };

            return model;
        }

        public async Task AddProduct(ProductBindingModel model)
        {

            var dbModel = this.Mapper.Map<Product>(model);

            using (var memoryStream = new MemoryStream())
            {
                foreach (var photo in model.Photos)
                {
                    await photo.CopyToAsync(memoryStream);
                    var dbPhoto = new Photo() { Data = memoryStream.ToArray() };
                    dbModel.Photos.Add(dbPhoto);
                }
            }

            await this.DbContext.Products.AddAsync(dbModel);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts(string subcategoryId)
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

            var models = this.Mapper.Map<List<ProductViewModel>>(subcategory.Products);

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

            var model = this.Mapper.Map<ProductBindingModel>(product);
            model.SubCategoryName = product.SubCategory.Name;

            return model;
        }

        public async Task<bool> Edit(ProductBindingModel model, string productId)
        {
            if (productId == null)
            {
                return false;
            }

            var dbModel = await this.DbContext.Products.FindAsync(productId);

            if (dbModel == null)
            {
                return false;
            }

            this.Mapper.Map(model, dbModel);

            if (model.Photos.Count > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    dbModel.Photos = new List<Photo>();

                    foreach (var photo in model.Photos)
                    {
                        await photo.CopyToAsync(memoryStream);
                        var dbPhoto = new Photo() { Data = memoryStream.ToArray() };
                        dbModel.Photos.Add(dbPhoto);
                    }
                }
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
    }
}
