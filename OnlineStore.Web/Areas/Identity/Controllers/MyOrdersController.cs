using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.UserServices.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    public class MyOrdersController : BaseIdentityController
    {
        private readonly IUserMyOrdersService userMyOrdersService;

        public MyOrdersController(IUserMyOrdersService userMyOrdersService)
        {
            this.userMyOrdersService = userMyOrdersService;
        }

        public IActionResult Index()
        {
            var model = this.userMyOrdersService.PrepareIndexModelForDisplaying(this.User);

            return View(model);
        }
    }
}