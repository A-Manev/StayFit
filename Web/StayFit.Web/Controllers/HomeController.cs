namespace StayFit.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels;
    using StayFit.Web.ViewModels.Home;
    using StayFit.Web.ViewModels.Users;

    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IExerciseScraperService scraperService;
        private readonly IMealScraperService mealScraperService;
        private readonly IUsersService usersService;
        private readonly IWebHostEnvironment environment;
        private readonly IMealService mealService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IExerciseScraperService scraperService,
            IMealScraperService mealScraperService,
            IUsersService usersService,
            IWebHostEnvironment environment,
            IMealService mealService)
        {
            this.userManager = userManager;

            this.scraperService = scraperService;
            this.mealScraperService = mealScraperService;
            this.usersService = usersService;
            this.environment = environment;
            this.mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            // Move to administration area.
            // await this.mealScraperService.PopulateDbWithMeal(2);
            // await this.scraperService.PopulateDbWithExercises(2);
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);

                var viewModel = this.usersService.GetById<HomePageUserViewModel>(user.Id);

                return this.View(viewModel);
            }
            else
            {
                var viewModel = new HomePageUserViewModel();

                viewModel.RandomMeals = this.mealService.GetRandom<HomePageMealsViewModel>(3);

                return this.View(viewModel);
            }
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult StatusCodeError(int errorCode)
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
