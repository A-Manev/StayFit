namespace StayFit.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Hangfire;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
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
        private readonly IUsersService usersService;
        private readonly IWebHostEnvironment environment;

        public HomeController(
            IHomeService homeService,
            UserManager<ApplicationUser> userManager,
            IExerciseScraperService scraperService,
            IMealScraperService mealService,
            IUsersService usersService,
            IWebHostEnvironment environment)
        {
            this.homeService = homeService;
            this.userManager = userManager;

            this.scraperService = scraperService;
            this.mealService = mealService;
            this.usersService = usersService;
            this.environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            // Move to administration area.
            // await this.mealService.PopulateDbWithMeal(2);
            // await this.scraperService.PopulateDbWithExercises(2);
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);

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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadUserImage(HomePageUserViewModel inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.usersService.UploadImageAsync(inputModel, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (System.Exception)
            {
                return this.Conflict();
            }

            return this.Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
