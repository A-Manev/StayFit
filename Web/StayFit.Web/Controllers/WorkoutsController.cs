namespace StayFit.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Exercises;

    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutService workoutService;

        public WorkoutsController(UserManager<ApplicationUser> userManager, IWorkoutService workoutService)
        {
            this.userManager = userManager;
            this.workoutService = workoutService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Workouts(List<ExerciseInputModel> inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.workoutService.CreateAsync(inputModel, user.Id);

            return this.Ok();
        }
    }
}
