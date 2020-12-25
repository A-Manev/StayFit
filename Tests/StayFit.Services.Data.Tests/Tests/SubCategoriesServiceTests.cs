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

    public class SubCategoriesServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<SubCategory>> subCategoryRepository;

        public SubCategoriesServiceTests()
        {
            this.subCategoryRepository = new Mock<IDeletableEntityRepository<SubCategory>>();
            AutoMapperConfig.RegisterMappings(Assembly.Load("StayFit.Web.ViewModels"));
        }

        // [Fact]
        public void GetAllSubCategoriesShouldReturnCorectNumberOfCount()
        {
            var service = new SubCategoriesService(this.subCategoryRepository.Object);

            this.subCategoryRepository
                .Setup(x => x.AllAsNoTracking())
                .Returns(new List<SubCategory>
                {
                    new SubCategory
                    {
                         Id = 1,
                         Name = "Test Name",
                    },
                }
                .AsQueryable());

            var subCategories = service.GetAllSubCategories();

            Assert.Single(subCategories);
        }

        // [Fact]
        public void GetAllSubCategoriesWithParameterShouldReturnCorectNumberOfCount()
        {
            var service = new SubCategoriesService(this.subCategoryRepository.Object);

            this.subCategoryRepository
                .Setup(x => x.AllAsNoTracking())
                .Returns(new List<SubCategory>
                {
                    new SubCategory
                    {
                         Id = 1,
                         Name = "Test Name",
                         CategoryId = 1,
                         Category = new Category { Id = 1,  Name = "Test Name", },
                    },
                }
                .AsQueryable());

            var subCategories = service.GetAllSubCategories(1);

            Assert.Single(subCategories);
        }
    }
}
