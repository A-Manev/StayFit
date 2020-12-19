namespace StayFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDiariesService
    {
        Task AddMealToDiaryAsync(int mealId, string userId, double quantity);

        IEnumerable<T> GetUserFoodDiary<T>(string userId, DateTime date);

        IEnumerable<T> GetUserRecentMeals<T>(string userId);

        Task DeleteMealFromDiaryAsync(int mealId, string userId);

        IEnumerable<T> GetUserWorkoutDiary<T>(string userId, DateTime date);
    }
}
