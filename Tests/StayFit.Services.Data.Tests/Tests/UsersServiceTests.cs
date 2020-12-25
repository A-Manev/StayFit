namespace StayFit.Services.Data.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Moq;
    using StayFit.Data;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Data.Repositories;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Users;
    using Xunit;

    public class UsersServiceTests : BaseServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<ApplicationUser>> userRepository;
        private readonly Mock<IRepository<Image>> imageRepository;

        public UsersServiceTests()
        {
            this.userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            this.imageRepository = new Mock<IRepository<Image>>();
        }

        [Fact]
        public void TestGetById()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "userId",
                    FirstName = "Test",
                    LastName = "Testov",
                    Age = 20,
                    CurrentWeight = 100,
                    Height = 200,
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.Date,
                    ActivityLevel = ActivityLevel.VeryActive,
                    WeightLoseGain = WeightLoseGain.Maintain,
                    DailyCalories = 3000,
                    RemainingCalories = 3000,
                    Protein = 200,
                    RemainingProtein = 200,
                    Carbs = 400,
                    RemainingCarbs = 400,
                    Fat = 100,
                    RemainingFat = 100,
                },
            };

            this.userRepository
               .Setup(x => x.All())
               .Returns(users
               .AsQueryable());

            var service = new UsersService(this.userRepository.Object, this.imageRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(HomePageUserTestModel).Assembly);

            var result = service.GetById<HomePageUserTestModel>("userId");

            Assert.Equal("userId", result.Id);
            Assert.Equal("Test", result.FirstName);
            Assert.Equal("Testov", result.LastName);
            Assert.Equal(20, result.Age);
            Assert.Equal(100, result.CurrentWeight);
            Assert.Equal(200, result.Height);
            Assert.Equal(DateTime.UtcNow.Date, result.BirthDate);
            Assert.Equal(Gender.Male, result.Gender);
            Assert.Equal(ActivityLevel.VeryActive, result.ActivityLevel);
            Assert.Equal(WeightLoseGain.Maintain, result.WeightLoseGain);
            Assert.Equal(3000, result.DailyCalories);
            Assert.Equal(3000, result.RemainingCalories);
            Assert.Equal(200, result.Protein);
            Assert.Equal(200, result.RemainingProtein);
            Assert.Equal(400, result.Carbs);
            Assert.Equal(400, result.RemainingCarbs);
            Assert.Equal(100, result.Fat);
            Assert.Equal(100, result.RemainingFat);
        }

        [Fact]
        public void TestCalculateForMale()
        {
            var user = new ApplicationUser
            {
                Age = 20,
                CurrentWeight = 100,
                Height = 200,
            };

            var service = new UsersService(
                this.userRepository.Object,
                this.imageRepository.Object);

            var result = service.CalculateForMale(user, 1.9);

            Assert.Equal(3604.35, result);
        }

        [Fact]
        public void TestCalculateForFemale()
        {
            var user = new ApplicationUser
            {
                Age = 20,
                CurrentWeight = 100,
                Height = 200,
            };

            var service = new UsersService(
                this.userRepository.Object,
                this.imageRepository.Object);

            var result = service.CalculateForFemale(user, 1.9);

            Assert.Equal(3356.9500000000003, result);
        }

        [Fact]
        public async Task TestUploadImageAsync()
        {
            ApplicationDbContext db = GetDb();

            var user = new ApplicationUser
            {
                Id = "userId",
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var imageRepository = new EfRepository<Image>(db);

            var service = new UsersService(userRepository, imageRepository);

            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var inputModel = new HomePageUserViewModel
            {
                Image = file,
            };

            await service.UploadImageAsync(inputModel, "userId", "wwwroot/images/users");

            Assert.True(db.Users.Any(x => x.Images != null));
            Assert.Equal(1, db.Images.Count());
        }

        [Fact]
        public async Task TestUploadImageAsyncShouldThrowException()
        {
            ApplicationDbContext db = GetDb();

            var user = new ApplicationUser
            {
                Id = "userId",
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var imageRepository = new EfRepository<Image>(db);

            var service = new UsersService(userRepository, imageRepository);

            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.exe";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var inputModel = new HomePageUserViewModel
            {
                Image = file,
            };

            var ex = await Assert.ThrowsAsync<Exception>(async () => await service.UploadImageAsync(inputModel, "userId", "wwwroot/images/users"));

            Assert.Equal("Invalid image extension exe", ex.Message);
        }

        [Theory]

        // Maintain
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Maintain, 2595.3)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Maintain, 2222.0400000000004)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Maintain, 2847.5625)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Maintain, 2505.7675000000004)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Maintain, 3099.8250000000003)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Maintain, 2789.4950000000003)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Maintain, 3352.0875)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Maintain, 3073.2225000000003)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Maintain, 3604.35)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Maintain, 3356.9500000000003)]

        // Lose1kg
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Lose1kg, 1179.6818181818182)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Lose1kg, 1010.0181818181819)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Lose1kg, 1294.3465909090908)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Lose1kg, 1138.9852272727273)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose1kg, 1409.0113636363637)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose1kg, 1267.9522727272729)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Lose1kg, 1523.6761363636363)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Lose1kg, 1396.9193181818182)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Lose1kg, 1638.340909090909)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Lose1kg, 1525.8863636363637)]

        // Lose075kgs
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Lose075kgs, 1526.6470588235295)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Lose075kgs, 1307.0823529411769)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Lose075kgs, 1675.0367647058824)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Lose075kgs, 1473.9808823529415)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose075kgs, 1823.4264705882356)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose075kgs, 1640.8794117647062)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Lose075kgs, 1971.8161764705883)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Lose075kgs, 1807.7779411764709)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Lose075kgs, 2120.205882352941)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Lose075kgs, 1974.6764705882356)]

        // Lose05kgs
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Lose05kgs, 2359.3636363636365)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Lose05kgs, 2020.0363636363638)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Lose05kgs, 2588.6931818181815)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Lose05kgs, 2277.9704545454547)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose05kgs, 2818.0227272727275)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose05kgs, 2535.9045454545458)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Lose05kgs, 3047.3522727272725)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Lose05kgs, 2793.8386363636364)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Lose05kgs, 3276.681818181818)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Lose05kgs, 3051.7727272727275)]

        // Lose025kgs -
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Lose025kgs, 2162.75)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Lose025kgs, 1851.7000000000003)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Lose025kgs, 2372.96875)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Lose025kgs, 2088.1395833333336)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose025kgs, 2583.1875)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Lose025kgs, 2324.5791666666669)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Lose025kgs, 2793.40625)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Lose025kgs, 2561.01875)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Lose025kgs, 3003.625)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Lose025kgs, 2797.4583333333335)]

        // Gain025kgs -
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Gain025kgs, 3027.8500000000004)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Gain025kgs, 2592.3800000000006)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Gain025kgs, 3322.15625)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Gain025kgs, 2923.3954166666672)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Gain025kgs, 3616.4625000000005)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Gain025kgs, 3254.4108333333338)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Gain025kgs, 3910.76875)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Gain025kgs, 3585.4262500000004)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Gain025kgs, 4205.075)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Gain025kgs, 3916.4416666666671)]

        // Gain05kgs
        [InlineData(Gender.Male, ActivityLevel.Sedentary, WeightLoseGain.Gain05kgs, 2854.8300000000004)]
        [InlineData(Gender.Female, ActivityLevel.Sedentary, WeightLoseGain.Gain05kgs, 2444.2440000000006)]
        [InlineData(Gender.Male, ActivityLevel.LightlyActive, WeightLoseGain.Gain05kgs, 3132.3187500000004)]
        [InlineData(Gender.Female, ActivityLevel.LightlyActive, WeightLoseGain.Gain05kgs, 2756.3442500000006)]
        [InlineData(Gender.Male, ActivityLevel.ModeratelyActive, WeightLoseGain.Gain05kgs, 3409.8075000000008)]
        [InlineData(Gender.Female, ActivityLevel.ModeratelyActive, WeightLoseGain.Gain05kgs, 3068.4445000000005)]
        [InlineData(Gender.Male, ActivityLevel.VeryActive, WeightLoseGain.Gain05kgs, 3687.2962500000003)]
        [InlineData(Gender.Female, ActivityLevel.VeryActive, WeightLoseGain.Gain05kgs, 3380.5447500000005)]
        [InlineData(Gender.Male, ActivityLevel.ExtraActive, WeightLoseGain.Gain05kgs, 3964.7850000000003)]
        [InlineData(Gender.Female, ActivityLevel.ExtraActive, WeightLoseGain.Gain05kgs, 3692.6450000000004)]

        public void TestCalculateUserCalories(Gender gender, ActivityLevel activityLevel, WeightLoseGain weightLoseGain, double finalresult)
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                FirstName = "Test",
                LastName = "Testov",
                Age = 20,
                CurrentWeight = 100,
                Height = 200,
                Gender = gender,
                BirthDate = DateTime.UtcNow.Date,
                ActivityLevel = activityLevel,
                WeightLoseGain = weightLoseGain,
                DailyCalories = 3000,
                RemainingCalories = 3000,
                Protein = 200,
                RemainingProtein = 200,
                Carbs = 400,
                RemainingCarbs = 400,
                Fat = 100,
                RemainingFat = 100,
            };

            var service = new UsersService(
                this.userRepository.Object,
                this.imageRepository.Object);

            var result = service.CalculateUserCalories(user);

            Assert.Equal(finalresult, result);
        }

        public class HomePageUserTestModel : IMapFrom<ApplicationUser>
        {
            public string Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public int Age { get; set; }

            public double CurrentWeight { get; set; }

            public int Height { get; set; }

            public Gender Gender { get; set; }

            public DateTime BirthDate { get; set; }

            public ActivityLevel ActivityLevel { get; set; }

            public WeightLoseGain WeightLoseGain { get; set; }

            public double DailyCalories { get; set; }

            public double RemainingCalories { get; set; }

            public double Protein { get; set; }

            public double RemainingProtein { get; set; }

            public double Carbs { get; set; }

            public double RemainingCarbs { get; set; }

            public double Fat { get; set; }

            public double RemainingFat { get; set; }
        }
    }
}
