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

    public class LikesServiceTests : BaseServiceTests
    {
        private readonly Mock<IRepository<Like>> likesRepository;

        public LikesServiceTests()
        {
            this.likesRepository = new Mock<IRepository<Like>>();
        }

        [Fact]
        public async Task GetLikesShouldReturnLikesCount()
        {
            ApplicationDbContext db = GetDb();

            var repository = new EfRepository<Like>(db);

            var service = new LikesService(repository);

            var like = new Like
            {
                CommentId = 34,
                UserId = "userId",
            };

            await db.AddAsync(like);
            await db.SaveChangesAsync();

            var count = service.GetLikes(34);

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task LikeAsyncShouldReturnTrue()
        {
            this.likesRepository
                .Setup(x => x.All())
                .Returns(new List<Like>()
                {
                    new Like
                    {
                    },
                }.AsQueryable());

            var service = new LikesService(this.likesRepository.Object);
            var result = await service.LikeAsync(10, "userId");

            Assert.True(result);
        }

        [Fact]
        public async Task LikeAsyncShouldReturnFalse()
        {
            ApplicationDbContext db = GetDb();

            this.likesRepository.Setup(x => x.All())
                .Returns(new List<Like>()
                {
                    new Like
                    {
                        CommentId = 10,
                        UserId = "userId",
                    },
                }.AsQueryable());

            var service = new LikesService(this.likesRepository.Object);
            var result = await service.LikeAsync(10, "userId");

            Assert.False(result);
            Assert.Equal(0, db.Likes.Count());
        }

        [Fact]
        public async Task CreateLikeShouldReturnTrue()
        {
            ApplicationDbContext db = GetDb();

            var likesRepository = new EfRepository<Like>(db);

            var service = new LikesService(likesRepository);

            await service.LikeAsync(1, "userId");

            Assert.Equal(1, db.Likes.Count());
        }
    }
}
