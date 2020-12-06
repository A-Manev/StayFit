namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDiariesService
    {
        Task AddMealToDiaryAsync(int mealId, string userId, double quantity);

        IEnumerable<T> GetUserFoodDiary<T>(string userId);
    }
}
