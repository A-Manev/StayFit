namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task LikeAsync(int commentId, string userId);

        int GetLikes(int commentId);
    }
}
