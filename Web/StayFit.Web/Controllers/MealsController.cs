namespace StayFit.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Meals;

    public class MealsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly ISubCategoriesService subCategoriesService;
        private readonly IMealService mealService;

        public MealsController(UserManager<ApplicationUser> userManager, ICategoriesService categoriesService, ISubCategoriesService subCategoriesService, IMealService mealService)
        {
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.subCategoriesService = subCategoriesService;
            this.mealService = mealService;
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
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CreateMealViewModel();

                viewModel.CategoriesItems = this.categoriesService.GetAllCategories();

                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.mealService.CreateAsync(inputModel, user.Id);

            return this.Redirect("/");
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
                MealsCount = this.mealService.GetAllMealsCount(),
                Meals = this.mealService.GetAll(id),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(viewModel);
        }
    }
}
