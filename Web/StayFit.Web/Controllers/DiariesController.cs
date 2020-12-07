﻿namespace StayFit.Web.Controllers
{
    using System;
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

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> FoodDiary()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new FoodDiaryListViewModel
            {
                Diary = this.diariesService.GetUserFoodDiary<FoodDiaryInListViewModel>(user.Id, DateTime.UtcNow.Date),
                CurrentDate = DateTime.UtcNow,
                User = this.usersService.GetById<UserCaloriesGoalViewModel>(user.Id),
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
    }
}
