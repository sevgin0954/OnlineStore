using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models.WebModels.Account.BindingModels;
using OnlineStore.Services.UserServices.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    public class AccountController : BaseIdentityController
    {
        private readonly IUserProfileService userProfileService;

        public AccountController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.userProfileService.PrepareIndexForEditingAsync(this.User);

            if (model == null)
            {
                AddStatusMessage(ControllerConstats.ErrorMessageUnknownError, ControllerConstats.MessageTypeDanger);
                return this.View();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var model = await this.userProfileService.PreparePersonInfoForEditingAsync(this.User);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonInfoBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
                return this.View();
            }

            await this.userProfileService.EditPersonInfoAsync(this.User, model);

            this.AddStatusMessage("Succesfully Edited", ControllerConstats.MessageTypeSuccess);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile image)
        {
            if (image == null)
            {
                AddStatusMessage(ControllerConstats.ErrorMessageUnknownError, ControllerConstats.MessageTypeDanger);
                return this.RedirectToAction("Index");
            }

            var contentType = image.ContentType;

            if (contentType != "image/jpeg")
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongPictureFormat, ControllerConstats.MessageTypeDanger);
                return this.RedirectToAction("Index");
            }

            var contentLength = image.Length;

            if (contentLength > 1000000)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageMaxSize, ControllerConstats.MessageTypeDanger);
                return this.RedirectToAction("Index");
            }

            await this.userProfileService.UpdateProfilePictureAsync(User, image);

            AddStatusMessage(ControllerConstats.MessageProfilePictureUpdated, ControllerConstats.MessageTypeSuccess);
            return this.RedirectToAction("Index");
        }
    }
}