namespace StayFit.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Hangfire;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels;
    using StayFit.Web.ViewModels.Users;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IExerciseScraperService scraperService;
        private readonly IMealScraperService mealService;
        private readonly IBackgroundJobClient backgroundJobs;

        public HomeController(IHomeService homeService, UserManager<ApplicationUser> userManager, IExerciseScraperService scraperService, IMealScraperService mealService, IBackgroundJobClient backgroundJobs)
        {
            this.homeService = homeService;
            this.userManager = userManager;

            this.scraperService = scraperService;
            this.mealService = mealService;
            this.backgroundJobs = backgroundJobs;
        }

        public async Task<IActionResult> Index()
        {
            // Move to administration area.
            // await this.mealService.PopulateDbWithMeal(2);
            // await this.scraperService.PopulateDbWithExercises(2);

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);

                //this.backgroundJobs.AddOrUpdate(() => this.homeService.ChangeUserCalories(user.Id), Cron.Minutely());

                RecurringJob.AddOrUpdate(() => this.homeService.ChangeUserCalories(user.Id), Cron.Daily());

                var viewModel = this.homeService.GetUserInfo<HomePageUserViewModel>(user.Id);

                return this.View(viewModel);
            }
            else
            {
                return this.View();
            }
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
