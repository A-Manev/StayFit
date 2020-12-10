namespace StayFit.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class LikesService : ILikesService
    {
        private readonly IRepository<Like> likesRepository;

        public LikesService(IRepository<Like> likesRepository)
        {
            this.likesRepository = likesRepository;
        }

        public int GetLikes(int commentId)
        {
            return this.likesRepository
                .All()
                .Where(x => x.CommentId == commentId)
                .Count();
        }

        public async Task LikeAsync(int commentId, string userId)
        {
            var like = this.likesRepository
                .All()
                .FirstOrDefault(x => x.CommentId == commentId && x.UserId == userId);

            if (like != null)
            {
                this.likesRepository.Delete(like);
            }
            else
            {
                like = new Like
                {
                    CommentId = commentId,
                    UserId = userId,
                    IsLiked = true,
                };

                await this.likesRepository.AddAsync(like);
            }

            await this.likesRepository.SaveChangesAsync();
        }
    }
}
