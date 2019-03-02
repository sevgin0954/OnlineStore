using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models.WebModels.DeliveryInfo.BindingModels;
using OnlineStore.Services.UserServices.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    public class DeliveryInfoController : BaseIdentityController
    {
        private readonly IUserDeliveryInfoService userDeliveryInfoService;

        public DeliveryInfoController(IUserDeliveryInfoService userDeliveryInfoService)
        {
            this.userDeliveryInfoService = userDeliveryInfoService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = this.userDeliveryInfoService.PrepareDeliveryInfoModelForAdding();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DeliveryInfoBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(this.ModelState);
                return this.RedirectToAction("Add");
            }

            await this.userDeliveryInfoService.AddDeliveryInfoToUserAsync(this.User, model);

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = this.userDeliveryInfoService.PrepareDeliveryInfoModelForEditing(this.User, id);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return RedirectToAction("Index", "Account");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DeliveryInfoBindingModel model, string id)
        {
            if (this.ModelState.IsValid == false)
            {
                this.AddStatusMessage(this.ModelState);
                return this.RedirectToAction("Edit");
            }

            var isSuccess = await this.userDeliveryInfoService.EditDeliveryInfoAsync(this.User, model, id);

            if (isSuccess == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return this.RedirectToAction("Edit");
            }

            return this.Redirect("/Identity/Account/Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var isSuccess = this.userDeliveryInfoService.DeleteDeliveryInfo(this.User, id);

            if (isSuccess == false)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
            }
            else
            {
                this.AddStatusMessage("Succefully deleted", ControllerConstats.MessageTypeSuccess);
            }

            return this.Redirect("/Identity/Account/Index");
        }

        [HttpGet]
        public async Task<JsonResult> GetPopulatedPlacesByDistrict(string districtId)
        {
            var populatedPlacesIds = await this.userDeliveryInfoService.GetPopulatedPlacesByDistrictAsync(districtId);

            return Json(populatedPlacesIds);
        }
    }
}