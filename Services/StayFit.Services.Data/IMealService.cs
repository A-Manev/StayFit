namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Meals;

    public interface IMealService
    {
        Task CreateAsync(CreateMealInputModel inputModel, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 15);

        int GetAllMealsCount();

        T GetMealDetails<T>(int mealId);

        (IEnumerable<T> Meals, int Count) GetAllSearched<T>(SearchMealInputModel input, int page, int itemsPerPage = 15);

        IEnumerable<T> GetRandom<T>(int count);
    }
}
