namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task CreateAsync(int mealId, string userId, string content, int? parentId = null);

        bool IsInMealId(int commentId, int mealId);
    }
}
