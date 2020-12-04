namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int mealId, string userId, byte value);

        double GetAverageVotes(int recipeId);
    }
}
