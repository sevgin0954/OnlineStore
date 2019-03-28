using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Models.WebModels.Admin.BindingModels;
using OnlineStore.Services.Admin.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class CategoriesController : BaseAdminController
    {
        private readonly IAdminCategoriesService adminCategoriesService;

        public CategoriesController(IAdminCategoriesService adminCategoriesService)
        {
            this.adminCategoriesService = adminCategoriesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var models = this.adminCategoriesService.GetAllCategories();

            return View(models);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
            }
            else
            {
                await this.adminCategoriesService.CreateCategoryAsync(model);
                this.AddStatusMessage(ControllerConstats.MessageSuccefullyCreated, ControllerConstats.MessageTypeSuccess);
            }

            return Redirect("/Admin/Categories");
        }

        [HttpGet]
        public async Task<IActionResult> AddSubcategory(string categoryId)
        {
            var model = await this.adminCategoriesService.PrepareSubCategoryModelForAdding(categoryId);

            if (model == null)
            {
                this.AddStatusMessage(ControllerConstats.ErrorMessageWrongId, ControllerConstats.MessageTypeDanger);
                return RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubcategory(SubCategoryCategoryBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                this.AddStatusMessage(ModelState);
                return this.View();
            }

            await this.adminCategoriesService.AddSubcategory(model);

            this.AddStatusMessage(ControllerConstats.MessageSuccefullyAdded, ControllerConstats.MessageTypeSuccess);

            return this.RedirectToAction("Index");
        }
    }
}