namespace StayFit.Services.Data.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using StayFit.Data;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Repositories;
    using StayFit.Services.Mapping;
    using Xunit;

    public class DiariesServiceTests : BaseServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<MealDiary>> mealsDiaryRepository;
        private readonly Mock<IDeletableEntityRepository<ApplicationUser>> userRepository;
        private readonly Mock<IDeletableEntityRepository<Meal>> mealRepository;
        private readonly Mock<IRepository<WorkoutExercise>> workoutExerciseRepository;

        public DiariesServiceTests()
        {
            this.mealsDiaryRepository = new Mock<IDeletableEntityRepository<MealDiary>>();
            this.userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            this.mealRepository = new Mock<IDeletableEntityRepository<Meal>>();
            this.workoutExerciseRepository = new Mock<IRepository<WorkoutExercise>>();
        }

        [Fact]
        public async Task TestAddMealToDiaryAsyncShouldReturnCorectly()
        {
            ApplicationDbContext db = GetDb();

            var mealsDiaryRepository = new EfDeletableEntityRepository<MealDiary>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var mealRepository = new EfDeletableEntityRepository<Meal>(db);
            var workoutExerciseRepository = new EfRepository<WorkoutExercise>(db);

            var service = new DiariesService(
                mealsDiaryRepository,
                userRepository,
                mealRepository,
                workoutExerciseRepository);

            var user = new ApplicationUser
            {
                Id = "userId",
                RemainingCalories = 1000,
                RemainingProtein = 1000,
                RemainingCarbs = 1000,
                RemainingFat = 1000,
            };

            var meal = new Meal
            {
                Id = 1,
                KCal = 100,
                Protein = 100,
                Carbs = 100,
                Fat = 100,
            };

            await db.Users.AddAsync(user);
            await db.Meals.AddAsync(meal);
            await db.SaveChangesAsync();

            await service.AddMealToDiaryAsync(1, "userId", 2.0);

            Assert.Single(db.MealsDiary.Where(x => x.UserId == "userId" && x.MealId == 1));
            Assert.Equal(800, user.RemainingCalories);
            Assert.Equal(800, user.RemainingProtein);
            Assert.Equal(800, user.RemainingCarbs);
            Assert.Equal(800, user.RemainingFat);
        }

        [Fact]
        public async Task TesDeleteMealFromDiaryAsyncShouldReturnCorectly()
        {
            ApplicationDbContext db = GetDb();

            var mealsDiaryRepository = new EfDeletableEntityRepository<MealDiary>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var mealRepository = new EfDeletableEntityRepository<Meal>(db);
            var workoutExerciseRepository = new EfRepository<WorkoutExercise>(db);

            var service = new DiariesService(
                mealsDiaryRepository,
                userRepository,
                mealRepository,
                workoutExerciseRepository);

            var user = new ApplicationUser
            {
                Id = "userId",
                RemainingCalories = 800,
                RemainingProtein = 800,
                RemainingCarbs = 800,
                RemainingFat = 800,
                DailyCalories = 1800,
                Protein = 1800,
                Carbs = 1800,
                Fat = 1800,
            };

            var meal = new Meal
            {
                Id = 1,
                KCal = 100,
                Protein = 100,
                Carbs = 100,
                Fat = 100,
            };

            var mealDiary = new MealDiary
            {
                MealId = 1,
                UserId = "userId",
                MealQuantity = 2,
            };

            var mealDiaryTwo = new MealDiary
            {
                MealId = 2,
                UserId = "userId",
            };

            await db.Users.AddAsync(user);
            await db.Meals.AddAsync(meal);
            await db.MealsDiary.AddAsync(mealDiary);
            await db.MealsDiary.AddAsync(mealDiaryTwo);
            await db.SaveChangesAsync();

            await service.DeleteMealFromDiaryAsync(1, "userId");

            Assert.Single(db.MealsDiary.Where(x => x.UserId == "userId" && x.MealId == 2));
            Assert.Equal(1000, user.RemainingCalories);
            Assert.Equal(1000, user.RemainingProtein);
            Assert.Equal(1000, user.RemainingCarbs);
            Assert.Equal(1000, user.RemainingFat);
        }

        [Fact]
        public async Task TesDeleteMealFromDiaryAsyncShouldReturnCorectlyWhenDailyCaloriesAreLessThanRemainingCalories()
        {
            ApplicationDbContext db = GetDb();

            var mealsDiaryRepository = new EfDeletableEntityRepository<MealDiary>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var mealRepository = new EfDeletableEntityRepository<Meal>(db);
            var workoutExerciseRepository = new EfRepository<WorkoutExercise>(db);

            var service = new DiariesService(
                mealsDiaryRepository,
                userRepository,
                mealRepository,
                workoutExerciseRepository);

            var user = new ApplicationUser
            {
                Id = "userId",
                RemainingCalories = 800,
                RemainingProtein = 800,
                RemainingCarbs = 800,
                RemainingFat = 800,
                DailyCalories = 0,
                Protein = 0,
                Carbs = 0,
                Fat = 0,
            };

            var meal = new Meal
            {
                Id = 1,
                KCal = 100,
                Protein = 100,
                Carbs = 100,
                Fat = 100,
            };

            var mealDiary = new MealDiary
            {
                MealId = 1,
                UserId = "userId",
                MealQuantity = 2,
            };

            var mealDiaryTwo = new MealDiary
            {
                MealId = 2,
                UserId = "userId",
            };

            await db.Users.AddAsync(user);
            await db.Meals.AddAsync(meal);
            await db.MealsDiary.AddAsync(mealDiary);
            await db.MealsDiary.AddAsync(mealDiaryTwo);
            await db.SaveChangesAsync();

            await service.DeleteMealFromDiaryAsync(1, "userId");

            Assert.Single(db.MealsDiary.Where(x => x.UserId == "userId" && x.MealId == 2));
            Assert.Equal(800, user.RemainingCalories);
            Assert.Equal(800, user.RemainingProtein);
            Assert.Equal(800, user.RemainingCarbs);
            Assert.Equal(800, user.RemainingFat);
        }

        [Fact]
        public async Task TesDeleteMealFromDiaryAsyncShouldReturnCorectlyWhenCreatedOnIsInvalid()
        {
            ApplicationDbContext db = GetDb();

            var mealsDiaryRepository = new EfDeletableEntityRepository<MealDiary>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var mealRepository = new EfDeletableEntityRepository<Meal>(db);
            var workoutExerciseRepository = new EfRepository<WorkoutExercise>(db);

            var service = new DiariesService(
                mealsDiaryRepository,
                userRepository,
                mealRepository,
                workoutExerciseRepository);

            var user = new ApplicationUser
            {
                Id = "userId",
                RemainingCalories = 800,
                RemainingProtein = 800,
                RemainingCarbs = 800,
                RemainingFat = 800,
                DailyCalories = 0,
                Protein = 0,
                Carbs = 0,
                Fat = 0,
            };

            var meal = new Meal
            {
                Id = 1,
                KCal = 100,
                Protein = 100,
                Carbs = 100,
                Fat = 100,
            };

            var mealDiary = new MealDiary
            {
                MealId = 1,
                UserId = "userId",
                MealQuantity = 2,
                CreatedOn = DateTime.UtcNow.Date.AddDays(-1),
            };

            var mealDiaryTwo = new MealDiary
            {
                MealId = 2,
                UserId = "userId",
            };

            await db.Users.AddAsync(user);
            await db.Meals.AddAsync(meal);
            await db.MealsDiary.AddAsync(mealDiary);
            await db.MealsDiary.AddAsync(mealDiaryTwo);
            await db.SaveChangesAsync();

            await service.DeleteMealFromDiaryAsync(1, "userId");

            Assert.Single(db.MealsDiary.Where(x => x.UserId == "userId" && x.MealId == 2));
            Assert.Equal(800, user.RemainingCalories);
            Assert.Equal(800, user.RemainingProtein);
            Assert.Equal(800, user.RemainingCarbs);
            Assert.Equal(800, user.RemainingFat);
        }

        [Fact]
        public void TestGetUserFoodDiaryReturnCorectly()
        {
            var foodDiaries = new List<MealDiary>
            {
                new MealDiary
                {
                     Id = 1,
                     MealId = 1,
                     UserId = "userId",
                     MealQuantity = 1,
                     CreatedOn = DateTime.UtcNow.Date,
                },
                new MealDiary
                {
                     Id = 2,
                     MealId = 2,
                     UserId = "userId",
                     MealQuantity = 1,
                     CreatedOn = DateTime.UtcNow.Date,
                },
            };

            this.mealsDiaryRepository
                .Setup(x => x.All())
                .Returns(foodDiaries
                .AsQueryable());

            var service = new DiariesService(
                this.mealsDiaryRepository.Object,
                this.userRepository.Object,
                this.mealRepository.Object,
                this.workoutExerciseRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(FoodDiaryTestModel).Assembly);

            var result = service.GetUserFoodDiary<FoodDiaryTestModel>("userId", DateTime.UtcNow.Date);

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.FirstOrDefault().Id);
            Assert.Equal(1, result.FirstOrDefault().MealId);
        }

        [Fact]
        public void TestGetUserRecentMealsReturnCorectly()
        {
            var foodDiaries = new List<MealDiary>
            {
                new MealDiary
                {
                     Id = 83,
                     MealId = 1,
                     UserId = "userId",
                     MealQuantity = 1,
                     CreatedOn = DateTime.UtcNow.Date,
                },
            };

            this.mealsDiaryRepository
                .Setup(x => x.All())
                .Returns(foodDiaries
                .AsQueryable());

            var service = new DiariesService(
                this.mealsDiaryRepository.Object,
                this.userRepository.Object,
                this.mealRepository.Object,
                this.workoutExerciseRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(FoodDiaryTestModel).Assembly);

            var result = service.GetUserRecentMeals<FoodDiaryTestModel>("userId");

            Assert.Single(result);
            Assert.Equal(83, result.FirstOrDefault().Id);
            Assert.Equal(1, result.FirstOrDefault().MealId);
        }

        [Fact]
        public void TestGetUserWorkoutDiaryReturnCorectly()
        {
            var workoutDiaries = new List<WorkoutExercise>
            {
                new WorkoutExercise
                {
                     Workout = new Workout { CreatedOn = DateTime.UtcNow.Date, User = new ApplicationUser { Id = "userId" } },
                     Id = 1,
                     ExerciseId = 1,
                     Reps = 10,
                     Sets = 3,
                     Weight = 100,
                },
                new WorkoutExercise
                {
                     Workout = new Workout { CreatedOn = DateTime.UtcNow.Date, User = new ApplicationUser { Id = "userId" } },
                     Id = 2,
                     ExerciseId = 2,
                     Reps = 10,
                     Sets = 3,
                     Weight = 100,
                },
            };

            this.workoutExerciseRepository
                .Setup(x => x.All())
                .Returns(workoutDiaries
                .AsQueryable());

            var service = new DiariesService(
                this.mealsDiaryRepository.Object,
                this.userRepository.Object,
                this.mealRepository.Object,
                this.workoutExerciseRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(WorkoutDiaryInListViewModel).Assembly);

            var result = service.GetUserWorkoutDiary<WorkoutDiaryInListViewModel>("userId", DateTime.UtcNow.Date);

            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.FirstOrDefault().Id);
            Assert.Equal(1, result.FirstOrDefault().ExerciseId);
            Assert.Equal(10, result.FirstOrDefault().Reps);
            Assert.Equal(3, result.FirstOrDefault().Sets);
            Assert.Equal(100, result.FirstOrDefault().Weight);
        }

        public class FoodDiaryTestModel : IMapFrom<MealDiary>
        {
            public int Id { get; set; }

            public int MealId { get; set; }
        }

        public class WorkoutDiaryInListViewModel : IMapFrom<WorkoutExercise>
        {
            public int Id { get; set; }

            public int ExerciseId { get; set; }

            public int Reps { get; set; }

            public int Sets { get; set; }

            public decimal Weight { get; set; }
        }
    }
}
