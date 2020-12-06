namespace StayFit.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Diaries;

    public class DiariesController : Controller
    {
        private readonly IDiariesService diariesService;
        private readonly UserManager<ApplicationUser> userManager;

        public DiariesController(IDiariesService diariesService, UserManager<ApplicationUser> userManager)
        {
            this.diariesService = diariesService;
            this.userManager = userManager;
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

            var viewModel = this.diariesService.GetUserFoodDiary<FoodDiaryViewModel>(user.Id);

            return this.View(viewModel);
        }
    }
}
