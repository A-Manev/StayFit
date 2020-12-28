namespace StayFit.Services.Data.Tests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using StayFit.Data;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Data.Repositories;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Exercises;
    using Xunit;

    public class ExercisesServiceTests : BaseServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Exercise>> exerciseRepository;

        public ExercisesServiceTests()
        {
            this.exerciseRepository = new Mock<IDeletableEntityRepository<Exercise>>();
        }

        [Fact]
        public void TestGetExerciseDetailsShouldReturnCorrectly()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "BenchPress",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
            };

            this.exerciseRepository
               .Setup(x => x.AllAsNoTracking())
               .Returns(exercises
               .AsQueryable());

            var service = new ExercisesService(this.exerciseRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(TestExerciseModel).Assembly);

            var result = service.GetExerciseDetails<TestExerciseModel>(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Squat", result.Name);
            Assert.Equal("img", result.ImageUrl);
            Assert.Equal("the best exercise", result.Description);
            Assert.Equal("some test", result.Benefits);
            Assert.Equal(BodyPart.Quadriceps, result.BodyPart);
            Assert.Equal(Difficulty.Intermediate, result.Difficulty);
            Assert.Equal(ExerciseType.Strength, result.ExerciseType);
        }

        [Fact]
        public void TestGetAllShouldReturnCountCorrectly()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "BenchPress",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
            };

            this.exerciseRepository
               .Setup(x => x.AllAsNoTracking())
               .Returns(exercises
               .AsQueryable());

            var service = new ExercisesService(this.exerciseRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(TestExerciseModel).Assembly);

            var result = service.GetAll<TestExerciseModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void TestGetAllWithParametersShouldReturnCountCorrectly()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "BenchPress",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
            };

            this.exerciseRepository
               .Setup(x => x.AllAsNoTracking())
               .Returns(exercises
               .AsQueryable());

            var service = new ExercisesService(this.exerciseRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(TestExerciseModel).Assembly);

            var result = service.GetAll<TestExerciseModel>(1);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void TestGetCountShouldReturnCountCorrectly()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "BenchPress",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
            };

            this.exerciseRepository
               .Setup(x => x.All())
               .Returns(exercises
               .AsQueryable());

            var service = new ExercisesService(this.exerciseRepository.Object);

            var result = service.GetCount();

            Assert.Equal(2, result);
        }

        [Fact]
        public void TestGetByIdShouldReturnCorrectly()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "BenchPress",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
            };

            this.exerciseRepository
               .Setup(x => x.All())
               .Returns(exercises
               .AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(TestExerciseModel).Assembly);

            var service = new ExercisesService(this.exerciseRepository.Object);

            var result = service.GetById<TestExerciseModel>(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Squat", result.Name);
            Assert.Equal("img", result.ImageUrl);
            Assert.Equal("the best exercise", result.Description);
            Assert.Equal("some test", result.Benefits);
            Assert.Equal(BodyPart.Quadriceps, result.BodyPart);
            Assert.Equal(Difficulty.Intermediate, result.Difficulty);
            Assert.Equal(ExerciseType.Strength, result.ExerciseType);
        }

        [Fact]
        public void GetAllWithDeletedShouldReturnCountCorrectly()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
                new Exercise
                {
                    Id = 2,
                    Name = "BenchPress",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                },
            };

            this.exerciseRepository
               .Setup(x => x.AllWithDeleted())
               .Returns(exercises
               .AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(TestExerciseModel).Assembly);

            var service = new ExercisesService(this.exerciseRepository.Object);

            var result = service.GetAllWithDeleted<TestExerciseModel>(1);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllSearchedShouldReturnAllSearchedExercises()
        {
            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    EquipmentId = 1,
                    Benefits = "some test",
                    BodyPart = BodyPart.Quadriceps,
                    Description = "the best exercise",
                    Difficulty = Difficulty.Intermediate,
                    ExerciseType = ExerciseType.Strength,
                    ImageUrl = "img",
                    Equipment = new Equipment { Id = 1, Name = "Barbell" },
                },
            };

            var service = new ExercisesService(this.exerciseRepository.Object);

            this.exerciseRepository
               .Setup(x => x.All())
               .Returns(exercises
               .AsQueryable());

            var inputModel = new SearchExerciseInputModel
            {
                Name = "Squat",
                EquipmentId = 1,
                BodyPart = BodyPart.Quadriceps,
                Difficulty = Difficulty.Intermediate,
                ExerciseType = ExerciseType.Strength,
            };

            AutoMapperConfig.RegisterMappings(typeof(TestExerciseModel).Assembly);

            var result = service.GetAllSearched<TestExerciseModel>(inputModel, 1);

            Assert.Equal(1, result.Count);
            Assert.Equal("Squat", result.Exercises.FirstOrDefault().Name);
            Assert.Equal(1, result.Exercises.FirstOrDefault().EquipmentId);
            Assert.Equal(BodyPart.Quadriceps, result.Exercises.FirstOrDefault().BodyPart);
            Assert.Equal(Difficulty.Intermediate, result.Exercises.FirstOrDefault().Difficulty);
            Assert.Equal(ExerciseType.Strength, result.Exercises.FirstOrDefault().ExerciseType);
        }

        [Fact]
        public async Task TestCreateAsyncShouldReturnCorectly()
        {
            ApplicationDbContext db = GetDb();

            var exerciseRepositiry = new EfDeletableEntityRepository<Exercise>(db);

            var service = new ExercisesService(exerciseRepositiry);

            var inputModel = new CreateExerciseAdministratioInputModel
            {
                Name = "Squat",
                EquipmentId = 1,
                Benefits = "some test",
                BodyPart = BodyPart.Quadriceps,
                Description = "the best exercise",
                Difficulty = Difficulty.Intermediate,
                ExerciseType = ExerciseType.Strength,
                ImageUrl = "img",
            };

            await service.CreateAsync(inputModel);

            Assert.Single(db.Exercises.Where(x => x.Name == "Squat"));
        }

        [Fact]
        public async Task TestUpdateAsyncShouldReturnCorectly()
        {
            ApplicationDbContext db = GetDb();

            var exerciseRepositiry = new EfDeletableEntityRepository<Exercise>(db);

            var service = new ExercisesService(exerciseRepositiry);

            var exercise = new Exercise
            {
                Name = "Squat",
                EquipmentId = 1,
                Benefits = "some test",
                BodyPart = BodyPart.Quadriceps,
                Description = "the best exercise",
                Difficulty = Difficulty.Intermediate,
                ExerciseType = ExerciseType.Strength,
                ImageUrl = "img",
            };

            await db.Exercises.AddAsync(exercise);
            await db.SaveChangesAsync();

            var inputModel = new ExerciseAdministrationEditViewModel
            {
                Id = 1,
                Name = "Bench",
                EquipmentId = 2,
                Benefits = "test",
                BodyPart = BodyPart.Chest,
                Description = "top exercise",
                Difficulty = Difficulty.Beginner,
                ExerciseType = ExerciseType.Hypertrophy,
                ImageUrl = "img2",
            };

            await service.UpdateAsync(inputModel);

            var result = db.Exercises.Find(1);

            Assert.Equal("Bench", result.Name);
            Assert.Equal(2, result.EquipmentId);
            Assert.Equal("test", result.Benefits);
            Assert.Equal(BodyPart.Chest, result.BodyPart);
            Assert.Equal("top exercise", result.Description);
            Assert.Equal(Difficulty.Beginner, result.Difficulty);
            Assert.Equal(ExerciseType.Hypertrophy, result.ExerciseType);
            Assert.Equal("img2", result.ImageUrl);
        }

        [Fact]
        public async Task TestDeleteAsyncShouldReturnCorectly()
        {
            ApplicationDbContext db = GetDb();

            var exerciseRepositiry = new EfDeletableEntityRepository<Exercise>(db);

            var service = new ExercisesService(exerciseRepositiry);

            var exercise = new Exercise
            {
                Name = "Squat",
                EquipmentId = 1,
                Benefits = "some test",
                BodyPart = BodyPart.Quadriceps,
                Description = "the best exercise",
                Difficulty = Difficulty.Intermediate,
                ExerciseType = ExerciseType.Strength,
                ImageUrl = "img",
            };

            await db.Exercises.AddAsync(exercise);
            await db.SaveChangesAsync();

            await service.DeleteAsync(1);

            var result = db.Exercises.Count();

            Assert.Equal(0, result);
        }

        public class TestExerciseModel : IMapFrom<Exercise>
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string ImageUrl { get; set; }

            public string Description { get; set; }

            public BodyPart BodyPart { get; set; }

            public Difficulty Difficulty { get; set; }

            public ExerciseType ExerciseType { get; set; }

            public string Benefits { get; set; }

            public int EquipmentId { get; set; }
        }
    }
}
