namespace StayFit.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Likes;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService likesService;

        public LikesController(ILikesService likesService)
        {
            this.likesService = likesService;
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<LikeResponseModel>> Like(LikeInputModel inputModel)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var isLiked = await this.likesService.LikeAsync(inputModel.CommentId, userId);

            var likes = this.likesService.GetLikes(inputModel.CommentId);

            return new LikeResponseModel { LikesCount = likes, IsLiked = isLiked };
        }
    }
}
