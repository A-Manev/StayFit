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

        public DiariesService(IDeletableEntityRepository<MealDiary> mealsDiaryRepository, IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<Meal> mealRepository)
        {
            this.mealsDiaryRepository = mealsDiaryRepository;
            this.userRepository = userRepository;
            this.mealRepository = mealRepository;
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
                user.RemainingCalories += meal.KCal * currentMealInDiary.MealQuantity;
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
    }
}
