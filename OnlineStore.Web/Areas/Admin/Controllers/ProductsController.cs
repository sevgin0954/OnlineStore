using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Services.Admin.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class ProductsController : BaseAdminController
    {
        private readonly IAdminProductsService productsServices;

        public ProductsController(IAdminProductsService productsServices)
        {
            this.productsServices = productsServices;
        }

        [HttpGet]
        public IActionResult Add(string subcategoryId)
        {
            if (subcategoryId == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return this.View();
            }

            var model = this.productsServices.PrepareModelForAdding(subcategoryId);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return this.View();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
                return this.Redirect("Add");
            }

            var result = this.ValidateImages(model.Photos);

            if (result == false)
            {
                return this.Redirect("Add");
            }

            await this.productsServices.AddProductAsync(model);

            this.AddStatusMessage(ControllerConstats.MessageSuccefullyAdded, ControllerConstats.MessageTypeSuccess);

            return Redirect("/Admin/Categories");
        }

        [HttpGet]
        public async Task<IActionResult> ViewProducts(string subcategoryId)
        {
            if (subcategoryId == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return Redirect("/Admin/Categories");
            }

            var model = await this.productsServices.GetProductsAsync(subcategoryId);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return Redirect("/Admin/Categories");
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return this.Redirect("/Admin/Categories");
            }

            var model = await this.productsServices.PrepareModelForEditingAsync(id);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return this.Redirect("/Admin/Categories");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
                return this.RedirectToAction("Edit", new { model.ProductId });
            }

            var result = this.ValidateImages(model.Photos);

            if (result == false)
            {
                return this.RedirectToAction("Edit", new { model.ProductId });
            }

            result = await this.productsServices.EditAsync(model, model.ProductId);

            if (result == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageUnknownError, ControllerConstats.MessageTypeDanger);
                return this.RedirectToAction("Edit", new { model.ProductId });
            }

            this.AddStatusMessage(ControllerConstats.MessageSuccefullyEdited, ControllerConstats.MessageTypeSuccess);
            return RedirectToAction("ViewProducts", new { model.SubCategoryId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string productId)
        {
            var result = await this.productsServices.Delete(productId);

            if (result == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return RedirectToAction("Edit", new { productId });
            }   

            this.AddStatusMessage(ControllerConstats.MessageSuccefullyDeleted, ControllerConstats.MessageTypeSuccess);
            return Redirect("/Admin/Categories");
        }

        private bool ValidateImages(ICollection<IFormFile> photos)
        {
            foreach (var photo in photos)
            {
                var contentType = photo.ContentType;

                if (contentType != "image/jpeg")
                {
                    this.AddStatusMessage(ControllerConstats.ErrorMessageWrongPictureFormat, ControllerConstats.MessageTypeDanger);

                    return false;
                }

                var contentLength = photo.Length;

                if (contentLength > 1000000)
                {
                    this.AddStatusMessage(ControllerConstats.ErrorMessageMaxSize, ControllerConstats.MessageTypeDanger);

                    return false;
                }
            }

            return true;
        }
    }
}