namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task<bool> LikeAsync(int commentId, string userId);

        int GetLikes(int commentId);
    }
}
