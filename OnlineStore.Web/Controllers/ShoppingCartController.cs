using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models.WebModels.Quest.BindingModels;
using OnlineStore.Services.Quest.Interfaces;
using OnlineStore.Web.Areas;
using System.Threading.Tasks;

namespace OnlineStore.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.shoppingCartService.GetProductsAsync(this.HttpContext.Session);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexPost()
        {
            var model = await this.shoppingCartService.GetProductsAsync(this.HttpContext.Session);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageNoProductsInCart, ControllerConstats.MessageTypeDanger);

                return this.RedirectToAction("Index");
            }

            return this.Redirect("/Identity/Order/ChooseAddress");
        }

        [HttpPost]
        public async Task AddProduct(string productId)
        {
            await this.shoppingCartService.AddProductAsync(productId, this.HttpContext.Session);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductCount(ProductCardBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.AddStatusMessage(this.ModelState);
                return this.View();
            }

            await this.shoppingCartService.UpdateProductCountAsync(model, this.HttpContext.Session);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProduct(string productId)
        {
            var result = await this.shoppingCartService.RemoveProduct(productId, this.HttpContext.Session);

            if (result == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return RedirectToAction("Index");
            }

            this.AddStatusMessage(ControllerConstats.MessageSuccefullyRemoved, ControllerConstats.MessageTypeSuccess);

            return RedirectToAction("Index");
        }
    }
}