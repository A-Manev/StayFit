namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Meals;

    public interface IMealService
    {
        Task CreateAsync(CreateMealInputModel inputModel, string userId, string imagePath);

        IEnumerable<MealInListViewModel> GetAll(int page, int itemsPerPage = 15);

        int GetAllMealsCount();

        MealDetailsViewModel GetMealDetails(int mealId);
    }
}
