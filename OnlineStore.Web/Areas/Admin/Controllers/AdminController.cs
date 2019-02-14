using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.Admin.Interfaces;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class AdminController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = this.adminService.PrepareIndexModelForEditing();

            return View(model);
        }
    }
}