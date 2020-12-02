namespace StayFit.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using StayFit.Services;
    using StayFit.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IExerciseScraperService scraperService;
        private readonly IMealScraperService mealService;

        public HomeController(IExerciseScraperService scraperService, IMealScraperService mealService)
        {
            this.scraperService = scraperService;
            this.mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            // Move to administration area.
            // await this.mealService.PopulateDbWithMeal(2);
           // await this.scraperService.PopulateDbWithExercises(100);
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
