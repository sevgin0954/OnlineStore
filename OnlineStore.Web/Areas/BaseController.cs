using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineStore.Common.Constants;

namespace OnlineStore.Web.Areas
{
    public abstract class BaseController : Controller
    {
        protected void AddStatusMessage(string message, string type)
        {
            this.TempData[ControllerConstats.StatusMessagePrefix] = message;
            this.TempData[ControllerConstats.StatusMessageType] = type;
        }

        protected void AddStatusMessage(ModelStateDictionary modelState, string type = ControllerConstats.MessageTypeDanger)
        {
            this.TempData[ControllerConstats.StatusMessageType] = type;

            int errorId = 0;

            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    this.TempData[ControllerConstats.StatusMessagePrefix + errorId] = error.ErrorMessage;
                    errorId++;
                }
            }
        }

        protected void AddStatusMessage(IdentityResult identityResult, string type = ControllerConstats.MessageTypeDanger)
        {
            this.TempData[ControllerConstats.StatusMessageType] = type;

            int errorId = 0;

            foreach (var error in identityResult.Errors)
            {
                this.TempData[ControllerConstats.StatusMessagePrefix + errorId] = error.Description;
                errorId++;
            }
        }
    }
}