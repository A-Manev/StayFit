namespace StayFit.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Services.Messaging;
    using StayFit.Services.ViewRender;
    using StayFit.Web.ViewModels.Meals;

    public class MealsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IMealService mealService;
        private readonly IWebHostEnvironment environment;
        private readonly IEmailSender emailSender;
        private readonly IViewRenderService viewRenderService;

        public MealsController(
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            ISubCategoriesService subCategoriesService,
            IMealService mealService,
            IWebHostEnvironment environment,
            IEmailSender emailSender,
            IViewRenderService viewRenderService)
        {
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.subCategoriesService = subCategoriesService;
            this.mealService = mealService;
            this.environment = environment;
            this.emailSender = emailSender;
            this.viewRenderService = viewRenderService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateMealViewModel();

            viewModel.CategoriesItems = this.categoriesService.GetAllCategories();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateMealInputModel inputModel)
        {
            var viewModel = new CreateMealViewModel();

            if (!this.ModelState.IsValid)
            {
                viewModel.CategoriesItems = this.categoriesService.GetAllCategories();

                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.mealService.CreateAsync(inputModel, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception exception)
            {
                this.ModelState.AddModelError(string.Empty, exception.Message);

                viewModel.CategoriesItems = this.categoriesService.GetAllCategories();

                return this.View(viewModel);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult CategorySubCategories(int categoryId)
        {
            var subCategories = this.subCategoriesService.GetAllSubCategories(categoryId);

            return this.Json(subCategories);
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 15;
            var viewModel = new MealListViewModel
            {
                PageNumber = id,
                TotalCount = this.mealService.GetAllMealsCount(),
                Meals = this.mealService.GetAll<MealInListViewModel>(id),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(viewModel);
        }

        public IActionResult MealDetails(int id)
        {
            var viewModel = this.mealService.GetMealDetails<MealDetailsViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Index()
        {
            var viewModel = new SearchMealInputModel();

            viewModel.CategoriesItems = this.categoriesService.GetAllCategories();

            return this.View(viewModel);
        }

        public IActionResult Search(SearchMealInputModel inputModel, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 15;

            var result = this.mealService.GetAllSearched<MealInListViewModel>(inputModel, id);

            if (result.Count == 0)
            {
                return this.NotFound();
            }

            var viewModel = new MealListViewModel
            {
                PageNumber = id,
                TotalCount = result.Count,
                Meals = result.Meals,
                ItemsPerPage = ItemsPerPage,
                Name = inputModel.Name,
                CategoryId = inputModel.CategoryId,
                SubCategory = inputModel.SubCategory,
                SkillLevel = inputModel.SkillLevel,
                PortionCount = inputModel.PortionCount,
                PreparationTime = inputModel.PreparationTime,
                CookingTime = inputModel.CookingTime,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            // var html = this.viewRenderService.RenderToStringAsync(nameof(this.MealDetails), new Meal());

            var meal = this.mealService.GetMealDetails<MealInListViewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{meal.Name}</h1>");
            html.AppendLine($"<h3>{meal.CategoryName}</h3>");
            html.AppendLine($"<img src=\"{meal.ImageUrl}\" />");
            await this.emailSender.SendEmailAsync("trainsleepeatandstayfit@gmail.com", "StayFit", "sashopro13@gmail.com", meal.Name, html.ToString());
            return this.RedirectToAction(nameof(this.MealDetails), new { id });
        }
    }
}
