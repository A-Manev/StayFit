namespace StayFit.Services.Data.Tests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Moq;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using Xunit;

    public class CategoriesServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Category>> categoryRepository;

        public CategoriesServiceTests()
        {
            this.categoryRepository = new Mock<IDeletableEntityRepository<Category>>();
            AutoMapperConfig.RegisterMappings(Assembly.Load("StayFit.Web.ViewModels"));
        }

        // [Fact]
        public void GetAllCategoriesShouldReturnCorectNumberOfCount()
        {
            var service = new CategoriesService(this.categoryRepository.Object);

            this.categoryRepository
                .Setup(x => x.AllAsNoTracking())
                .Returns(new List<Category>
                {
                    new Category
                    {
                        Id = 1,
                        Name = "Test Name",
                    },
                }
                .AsQueryable());

            var categories = service.GetAllCategories();

            Assert.Single(categories);
        }
    }
}
