namespace StayFit.Services.Data.Tests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using StayFit.Data;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Repositories;
    using Xunit;

    public class VotesServiceTests : BaseServiceTests
    {
        private readonly Mock<IRepository<Vote>> votesRepository;

        public VotesServiceTests()
        {
            this.votesRepository = new Mock<IRepository<Vote>>();
        }

        [Fact]
        public async Task TestCreateNewVote()
        {
            ApplicationDbContext db = GetDb();

            var voteRepository = new EfRepository<Vote>(db);

            var service = new VotesService(voteRepository);

            await service.SetVoteAsync(1, "userId", 5);

            var result = db.Votes.FirstOrDefault(x => x.MealId == 1 && x.UserId == "userId");

            Assert.Single(db.Votes.Where(x => x.MealId == 1));
            Assert.Single(db.Votes.Where(x => x.UserId == "userId"));
            Assert.Equal(5, result.Value);
        }

        [Fact]
        public async Task WhenUserVotesTwoTimesOnlyOneVoteShouldBeCounted()
        {
            var list = new List<Vote>();

            this.votesRepository
                .Setup(x => x.All())
                .Returns(list.AsQueryable());

            var service = new VotesService(this.votesRepository.Object);

            list.Add(new Vote { MealId = 1, UserId = "userId", Value = 5 });

            await service.SetVoteAsync(1, "userId", 1);
            await service.SetVoteAsync(1, "userId", 5);

            var result = list.FirstOrDefault(x => x.MealId == 1);

            Assert.Single(list);
            Assert.Equal(5, result.Value);
        }

        [Fact]
        public async Task GetAverageVotesShouldReturnCorrectly()
        {
            ApplicationDbContext db = GetDb();

            var vote = new Vote() { MealId = 1, UserId = "userId", Value = 2 };
            var voteTwo = new Vote() { MealId = 1, UserId = "userId1", Value = 3 };
            var voteThree = new Vote() { MealId = 1, UserId = "userId3", Value = 5 };
            var voteFour = new Vote() { MealId = 2, UserId = "userId3", Value = 5 };

            await db.Votes.AddAsync(vote);
            await db.Votes.AddAsync(voteTwo);
            await db.Votes.AddAsync(voteThree);
            await db.Votes.AddAsync(voteFour);
            await db.SaveChangesAsync();

            var voteReposotory = new EfRepository<Vote>(db);

            var service = new VotesService(voteReposotory);

            var result = service.GetAverageVotes(1);

            Assert.Equal(3.3333333333333335, result);
        }
    }
}
