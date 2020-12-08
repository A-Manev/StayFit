namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task Create(int mealId, string userId, string content, int? parentId = null);

        //bool IsInPostId(int commentId, int mealId);
    }
}
