using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.UserServices.Interfaces;
using System.Threading.Tasks;

namespace OnlineStore.Web.Areas.Identity.Controllers
{
    public class FavoritesController : BaseIdentityController
    {
        private readonly IUserFavoritesService favoritesService;

        public FavoritesController(IUserFavoritesService favoritesService)
        {
            this.favoritesService = favoritesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.favoritesService.GetAllFavoriteProducts(this.User);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(string productId)
        {
            var result = await this.favoritesService.AddProductAsync(productId, this.User);

            if (result == false)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProduct(string productId)
        {
            var result = await this.favoritesService.RemoveProductAsync(productId, this.User);

            if (result == false)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Index");
        }
    }
}