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

    public class EquipmentServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Equipment>> equipmentRepository;

        public EquipmentServiceTests()
        {
            this.equipmentRepository = new Mock<IDeletableEntityRepository<Equipment>>();
            AutoMapperConfig.RegisterMappings(Assembly.Load("StayFit.Web.ViewModels"));
        }

        // [Fact]
        public void GetAllCategoriesShouldReturnCorectNumberOfCount()
        {
            var service = new EquipmentService(this.equipmentRepository.Object);

            this.equipmentRepository.Setup(x => x.AllAsNoTracking())
                .Returns(new List<Equipment>
                {
                    new Equipment
                    {
                        Id = 1,
                        Name = "Test Name",
                    },
                }
                .AsQueryable());

            var equipments = service.GetAllEquipments();

            Assert.Single(equipments);
        }
    }
}
