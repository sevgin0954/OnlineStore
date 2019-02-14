using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Services.Admin.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUsersService adminUsersService;

        public UsersController(IAdminUsersService adminUsersService)
        {
            this.adminUsersService = adminUsersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.adminUsersService.PrepareModelForEditingAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeState(string userId)
        {
            if (userId != null)
            {
                var result = await this.adminUsersService.ChangeStateAsync(userId);

                if (result.Succeeded == false)
                {
                    this.AddStatusMessage(result);
                }
                else
                {
                    this.AddStatusMessage(ControllerConstats.MessageStatusChangedSuccefully, ControllerConstats.MessageTypeSuccess);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            if (userId != null)
            {
                var result = await this.adminUsersService.Delete(userId);

                if (result.Succeeded == false)
                {
                    this.AddStatusMessage(result);
                }
                else
                {
                    this.AddStatusMessage(ControllerConstats.MessageDeletedUserSuccefully, ControllerConstats.MessageTypeSuccess);
                }
            }

            return RedirectToAction("Index");
        }
    }
}