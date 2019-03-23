using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.Admin.Interfaces;
using System.Threading.Tasks;
using OnlineStore.Common.Constants;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class OrdersController : BaseAdminController
    {
        private readonly IAdminOrderService adminOrderService;

        public OrdersController (IAdminOrderService adminOrderService)
        {
            this.adminOrderService = adminOrderService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await this.adminOrderService.GetAllOrdersAsync();

            return this.View(models);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string orderId)
        {
            var result = await this.adminOrderService.CancelOrderAsync(orderId);
            if (result == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
            }
            else
            {
                this.AddStatusMessage(ControllerConstats.MessageStatusChangedSuccefully, ControllerConstats.MessageTypeSuccess);
            }
            
            return this.RedirectToAction("Index");
        }
    }
}