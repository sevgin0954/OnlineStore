using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models.WebModels.OrderModels.BindingModels;
using OnlineStore.Services.UserServices.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    public class OrderController : BaseIdentityController
    {
        private readonly IUserOrderService userOrderService;

        public OrderController(IUserOrderService userOrderService)
        {
            this.userOrderService = userOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> ChooseAddress()
        {
            var model = await this.userOrderService
                .PrepareModelForChoosingAddressAsync(this.HttpContext.Session, this.User);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageNoProductsInCart, ControllerConstats.MessageTypeDanger);
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult ChooseAddress(string deliveryInfoId)
        {
            if (deliveryInfoId == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return this.Redirect("/ShoppingCart");
            }

            return this.RedirectToAction("Order", new { deliveryInfoId });
        }

        [HttpGet]
        public async Task<IActionResult> Order(string deliveryInfoId)
        {
            var model = 
                await this.userOrderService.PrepareModelForOrdering(this.User, this.HttpContext.Session, deliveryInfoId);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageUnknownError, ControllerConstats.MessageTypeDanger);

                return this.RedirectToAction("/ShoppingCart");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderBidningModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                this.AddStatusMessage(this.ModelState);

                return this.RedirectToAction("ChooseAddress");
            }

            var result = await this.userOrderService.CreateOrder(model, this.User, this.HttpContext.Session);

            if (result == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageUnknownError, ControllerConstats.MessageTypeDanger);

                return this.RedirectToAction("ChooseAddress");
            }

            this.AddStatusMessage(ControllerConstats.MessageSuccefullyOrdered, ControllerConstats.MessageTypeSuccess);

            return this.Redirect("/Identity/Account");
        }
    }
}