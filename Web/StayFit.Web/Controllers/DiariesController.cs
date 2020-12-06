namespace StayFit.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;

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
        public async Task<IActionResult> Add(int id/*, double quantity*/)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.diariesService.AddMealToDiary(id, user.Id, 1);

            return this.Redirect("/");
        }
    }
}
