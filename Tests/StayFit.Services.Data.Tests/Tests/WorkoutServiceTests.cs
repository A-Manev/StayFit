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
    using StayFit.Web.ViewModels.Exercises;
    using Xunit;

    public class WorkoutServiceTests : BaseServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Workout>> workoutRepository;
        private readonly Mock<IRepository<WorkoutExercise>> workoutExerciseRepository;
        private readonly Mock<IDeletableEntityRepository<Exercise>> exerciseRepository;

        public WorkoutServiceTests()
        {
            this.workoutRepository = new Mock<IDeletableEntityRepository<Workout>>();
            this.workoutExerciseRepository = new Mock<IRepository<WorkoutExercise>>();
            this.exerciseRepository = new Mock<IDeletableEntityRepository<Exercise>>();
        }

        [Fact]
        public async Task TestCreateAsyncShouldReturnNullReferenceException()
        {
            ApplicationDbContext db = GetDb();

            var workoutRepository = new EfDeletableEntityRepository<Workout>(db);
            var workoutExerciseRepository = new EfRepository<WorkoutExercise>(db);
            var exerciseRepository = new EfDeletableEntityRepository<Exercise>(db);

            var service = new WorkoutService(
                workoutRepository,
                workoutExerciseRepository,
                exerciseRepository);

            var exercises = new List<ExerciseInputModel>
            {
                new ExerciseInputModel
                {
                },
            };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateAsync(exercises, "userId"));

            Assert.Equal("Object reference not set to an instance of an object.", ex.Message);
        }

        [Fact]
        public async Task TestCreateAsyncShouldReturnCorrectly()
        {
            ApplicationDbContext db = GetDb();

            var workoutRepository = new EfDeletableEntityRepository<Workout>(db);
            var workoutExerciseRepository = new EfRepository<WorkoutExercise>(db);
            var exerciseRepository = new EfDeletableEntityRepository<Exercise>(db);

            var service = new WorkoutService(
                workoutRepository,
                workoutExerciseRepository,
                exerciseRepository);

            var exercise = new Exercise
            {
                Name = "Squat",
            };

            await db.Exercises.AddAsync(exercise);
            await db.SaveChangesAsync();

            var exercises = new List<ExerciseInputModel>
            {
                new ExerciseInputModel
                {
                     Name = "Squat",
                     Reps = 10,
                     Sets = 3,
                     Weight = 100,
                },
            };

            await service.CreateAsync(exercises, "userId");

            Assert.Single(db.WorkoutExercises.Where(x => x.ExerciseId == 1));
        }
    }
}
