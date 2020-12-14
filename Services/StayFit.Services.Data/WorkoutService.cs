namespace StayFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Web.ViewModels.Exercises;

    public class WorkoutService : IWorkoutService
    {
        private readonly IDeletableEntityRepository<Workout> workoutRepository;
        private readonly IRepository<WorkoutExercise> workoutExerciseRepository;
        private readonly IDeletableEntityRepository<Exercise> exerciseRepository;

        public WorkoutService(
            IDeletableEntityRepository<Workout> workoutRepository,
            IRepository<WorkoutExercise> workoutExerciseRepository,
            IDeletableEntityRepository<Exercise> exerciseRepository)
        {
            this.workoutRepository = workoutRepository;
            this.workoutExerciseRepository = workoutExerciseRepository;
            this.exerciseRepository = exerciseRepository;
        }

        public async Task CreateAsync(List<ExerciseInputModel> exercises, string userId)
        {
            var workout = new Workout
            {
                Name = "Test Workout Name",
                UserId = userId,
            };

            await this.workoutRepository.AddAsync(workout);

            foreach (var exercise in exercises)
            {
                var exerciseId = this.exerciseRepository
                    .All()
                    .Where(x => x.Name == exercise.Name)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                if (exerciseId == 0)
                {
                    throw new NullReferenceException();
                }

                var workoutExercise = new WorkoutExercise
                {
                    Workout = workout,
                    ExerciseId = exerciseId,
                    Reps = exercise.Reps,
                    Sets = exercise.Sets,
                    Weight = exercise.Weight,
                };

                await this.workoutExerciseRepository.AddAsync(workoutExercise);
            }

            await this.workoutRepository.SaveChangesAsync();
            await this.workoutExerciseRepository.SaveChangesAsync();
        }
    }
}
