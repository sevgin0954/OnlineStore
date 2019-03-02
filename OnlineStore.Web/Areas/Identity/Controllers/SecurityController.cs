using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models;
using OnlineStore.Models.WebModels.Security.BindingModels;
using OnlineStore.Services.UserServices.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    public class SecurityController : BaseIdentityController
    {
        private readonly IUserSecurityService userSecurityService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;

        public SecurityController(
            IUserSecurityService userSecurityService, 
            IEmailSender emailSender, 
            UserManager<User> userManager)
        {
            this.userSecurityService = userSecurityService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await userSecurityService.PrepareIndexModelForEditingAsync(this.User);

            return View(model);
        }

        public async Task<IActionResult> EditEmail(EmailBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
            }
            else
            {
                var result = await this.userSecurityService.EditEmailAsync(this.User, model);

                if (result.Succeeded == false)
                {
                    this.AddStatusMessage(result);
                }
                else
                {
                    this.AddStatusMessage(ControllerConstats.MessageEmailUpdated, ControllerConstats.MessageTypeSuccess);

                    await SendConfirmationEmail();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task SendConfirmationEmail()
        {
            var dbUser = await this.userManager.GetUserAsync(this.User);

            var code = await userManager.GenerateEmailConfirmationTokenAsync(dbUser);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = dbUser.Id, code = code },
                protocol: Request.Scheme);

            await emailSender.SendEmailAsync(dbUser.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        public async Task<IActionResult> EditPassword(PasswordBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
            }
            else
            {
                var result = await this.userSecurityService.EditPassword(this.User, model);

                if (result.Succeeded == false)
                {
                    this.AddStatusMessage(result);
                }
                else
                {
                    this.AddStatusMessage(ControllerConstats.MessagePasswordUpdated, ControllerConstats.MessageTypeSuccess);
                }
            }

            return RedirectToAction("Index");
        }
    }
}