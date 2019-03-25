using Microsoft.AspNetCore.Mvc;
using OnlineStore.Common.Constants;
using OnlineStore.Services.Quest.Interfaces;
using OnlineStore.Web.Areas;
using System.Threading.Tasks;

namespace OnlineStore.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IQuestHomeService questHomeServices;

        public ProductsController(IQuestHomeService questHomeServices)
        {
            this.questHomeServices = questHomeServices;
        }

        [HttpGet]
        public async Task<IActionResult> Products(string subcategoryId)
        {
            var models = await this.questHomeServices.GetProductsBySubcategoryAsync(subcategoryId);

            return this.View(models);
        }

        [HttpGet]
        public IActionResult Search(string searchWords)
        {
            if (string.IsNullOrEmpty(searchWords))
            {
                return this.Redirect("/");
            }

            var models = this.questHomeServices.GetProductsByKeywords(searchWords);

            return this.View(models);
        }
    }
}