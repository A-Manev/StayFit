namespace StayFit.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel inputModel)
        {
            var parentId = inputModel.ParentId == 0
                ? (int?)null
                : inputModel.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInMealId(parentId.Value, inputModel.MealId))
                {
                    return this.BadRequest();
                }
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.Create(inputModel.MealId, user.Id, inputModel.Content, parentId);

            return this.RedirectToAction("MealDetails", "Meals", new { id = inputModel.MealId });
        }
    }
}
