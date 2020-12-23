namespace StayFit.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(int mealId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                MealId = mealId,
                UserId = userId,
                Content = content,
                ParentId = parentId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInMealId(int commentId, int mealId)
        {
            var commentMealId = this.commentsRepository
                .All()
                .Where(x => x.Id == commentId)
                .Select(x => x.MealId)
                .FirstOrDefault();

            return commentMealId == mealId;
        }
    }
}
