namespace StayFit.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Diaries;
    using StayFit.Web.ViewModels.Users;

    public class DiariesController : Controller
    {
        private readonly IDiariesService diariesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public DiariesController(IDiariesService diariesService, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.diariesService = diariesService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> Add(int mealId, double quantity)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.diariesService.AddMealToDiaryAsync(mealId, user.Id, quantity);

            return this.Redirect(nameof(this.FoodDiary));
        }

        [Authorize]
        public async Task<IActionResult> FoodDiary(DateTime? date)
        {
            DateTime currentDate;

            if (date == null)
            {
                currentDate = DateTime.UtcNow.Date;
            }
            else
            {
                var inputDateToShortDateString = date.Value.Date.ToShortDateString();

                currentDate = DateTime.ParseExact(inputDateToShortDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new FoodDiaryListViewModel
            {
                CurrentDate = currentDate,
                User = this.usersService.GetById<UserCaloriesGoalViewModel>(user.Id),
                Diary = this.diariesService.GetUserFoodDiary<FoodDiaryInListViewModel>(user.Id, currentDate),
                RecentMeals = this.diariesService.GetUserRecentMeals<FoodDiaryInListViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.diariesService.DeleteMealFromDiaryAsync(id, user.Id);

            return this.RedirectToAction(nameof(this.FoodDiary));
        }

        public async Task<IActionResult> WorkoutDiary(DateTime? date)
        {
            DateTime currentDate;

            if (date == null)
            {
                currentDate = DateTime.UtcNow.Date;
            }
            else
            {
                var inputDateToShortDateString = date.Value.Date.ToShortDateString();

                currentDate = DateTime.ParseExact(inputDateToShortDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new WorkoutDiaryListViewModel
            {
                Diary = this.diariesService.GetUserWorkoutDiary<WorkoutDiaryInListViewModel>(user.Id, currentDate),
                CurrentDate = currentDate,
            };

            return this.View(viewModel);
        }
    }
}
