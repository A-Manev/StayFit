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

    public class CommentServiceTests : BaseServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Comment>> commentsRepository;

        public CommentServiceTests()
        {
            this.commentsRepository = new Mock<IDeletableEntityRepository<Comment>>();
        }

        [Fact]
        public async Task CreateAsyncShouldReturnCorrectly()
        {
            ApplicationDbContext db = GetDb();

            var repositiry = new EfDeletableEntityRepository<Comment>(db);

            var service = new CommentService(repositiry);

            await service.CreateAsync(1, "userId", "Test");

            Assert.Single(db.Comments.Where(x => x.Content == "Test"));
        }

        [Fact]
        public void IsInMealIdShouldReturnCorrectly()
        {
            var comments = new List<Comment>()
            {
                new Comment
                {
                    Id = 1,
                    Content = "Some text",
                    MealId = 1,
                },
                new Comment
                {
                    Id = 2,
                    Content = "Some text two",
                    MealId = 2,
                },
            };

            this.commentsRepository
              .Setup(x => x.All())
              .Returns(comments
              .AsQueryable());

            var service = new CommentService(this.commentsRepository.Object);

            var result = service.IsInMealId(1, 1);

            Assert.True(result);
        }
    }
}
