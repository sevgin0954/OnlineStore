using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services.Quest.Interfaces;

namespace OnlineStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestHomeService homeServices;

        public HomeController(IQuestHomeService homeServices)
        {
            this.homeServices = homeServices;
        }

        public IActionResult Index()
        {
            var model = this.homeServices.PrepareIndexModel();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
