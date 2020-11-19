namespace StayFit.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using StayFit.Services;
    using StayFit.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IExerciseScraperService scraperService;

        public HomeController(IExerciseScraperService scraperService)
        {
            this.scraperService = scraperService;
        }

        public IActionResult Index()
        {
            // Move to administration area.
            this.scraperService.PopulateDbWithExercises(2);

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
