namespace StayFit.Web.Controllers
{
    using System.Threading.Tasks;

    using Hangfire;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;

    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : BaseController
    {
        private readonly IUsersService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public HangfireController(IUsersService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> DatabaseUpdate()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            // RecurringJob.AddOrUpdate(() => this.userService.ChangeUserCalories(user.Id), Cron.Minutely());
            return this.Ok();
        }
    }
}
