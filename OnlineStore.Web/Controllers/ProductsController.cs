using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.Quest.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IQuestHomeServices questHomeServices;

        public ProductsController(IQuestHomeServices questHomeServices)
        {
            this.questHomeServices = questHomeServices;
        }

        [HttpGet]
        public async Task<IActionResult> Products(string subcategoryId)
        {
            var models = await this.questHomeServices.GetProductsBySubcategoryAsync(subcategoryId);

            return View(models);
        }

        [HttpGet]
        public IActionResult Search(string searchWords)
        {
            var models = this.questHomeServices.GetProductsByKeywords(searchWords);

            return View(models);
        }
    }
}