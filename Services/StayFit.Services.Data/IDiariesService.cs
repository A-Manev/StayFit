namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Diaries;

    public interface IDiariesService
    {
        Task AddMealToDiaryAsync(int mealId, string userId, double quantity);

        IEnumerable<FoodDiaryViewModel> GetUserFoodDiary(string userId);
    }
}
