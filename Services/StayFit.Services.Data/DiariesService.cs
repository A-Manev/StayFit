namespace StayFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class DiariesService : IDiariesService
    {
        private readonly IDeletableEntityRepository<MealDiary> mealsDiaryRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Meal> mealRepository;
        private readonly IRepository<WorkoutExercise> workoutExerciseRepository;

        public DiariesService(
            IDeletableEntityRepository<MealDiary> mealsDiaryRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Meal> mealRepository,
            IRepository<WorkoutExercise> workoutExerciseRepository)
        {
            this.mealsDiaryRepository = mealsDiaryRepository;
            this.userRepository = userRepository;
            this.mealRepository = mealRepository;
            this.workoutExerciseRepository = workoutExerciseRepository;
        }

        public async Task AddMealToDiaryAsync(int mealId, string userId, double quantity)
        {
            var user = this.userRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var meal = this.mealRepository
                .AllAsNoTracking()
                .Where(x => x.Id == mealId)
                .FirstOrDefault();

            user.RemainingCalories -= meal.KCal * quantity;
            user.RemainingProtein -= meal.Protein * quantity;
            user.RemainingCarbs -= meal.Carbs * quantity;
            user.RemainingFat -= meal.Fat * quantity;

            var mealDiary = new MealDiary
            {
                MealId = mealId,
                UserId = userId,
                MealQuantity = quantity,
            };

            await this.mealsDiaryRepository.AddAsync(mealDiary);
            await this.mealsDiaryRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }

        public async Task DeleteMealFromDiaryAsync(int id, string userId)
        {
            var user = this.userRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var currentMealInDiary = this.mealsDiaryRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var meal = this.mealRepository
                .AllAsNoTracking()
                .Where(x => x.Id == currentMealInDiary.MealId)
                .FirstOrDefault();

            if (currentMealInDiary.CreatedOn.Date == DateTime.UtcNow.Date)
            {
                if (user.RemainingCalories + (meal.KCal * currentMealInDiary.MealQuantity) <= user.DailyCalories &&
                    user.RemainingProtein + (meal.Protein * currentMealInDiary.MealQuantity) <= user.Protein &&
                    user.RemainingCarbs + (meal.Carbs * currentMealInDiary.MealQuantity) <= user.Carbs &&
                    user.RemainingFat + (meal.Fat * currentMealInDiary.MealQuantity) <= user.Fat)
                {
                    user.RemainingCalories += meal.KCal * currentMealInDiary.MealQuantity;
                    user.RemainingProtein += meal.Protein * currentMealInDiary.MealQuantity;
                    user.RemainingCarbs += meal.Carbs * currentMealInDiary.MealQuantity;
                    user.RemainingFat += meal.Fat * currentMealInDiary.MealQuantity;
                }
            }

            this.mealsDiaryRepository.Delete(currentMealInDiary);

            await this.userRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetUserFoodDiary<T>(string userId, DateTime date)
        {
            return this.mealsDiaryRepository
                .All()
                .Where(x => x.UserId == userId && x.CreatedOn.Date == date.Date)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetUserRecentMeals<T>(string userId)
        {
            return this.mealsDiaryRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(20)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetUserWorkoutDiary<T>(string userId, DateTime date)
        {
            return this.workoutExerciseRepository
                .All()
                .Where(x => x.Workout.User.Id == userId && x.Workout.CreatedOn.Date == date)
                .To<T>()
                .ToList();
        }
    }
}
