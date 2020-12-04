namespace StayFit.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(int mealId)
        {
            return this.votesRepository.All()
                .Where(x => x.MealId == mealId)
                .Average(x => x.Value);
        }

        public async Task SetVoteAsync(int mealId, string userId, byte value)
        {
            var vote = this.votesRepository.All().FirstOrDefault(x => x.MealId == mealId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    MealId = mealId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
