namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Meals;

    public interface IMealService
    {
        Task CreateAsync(CreateMealInputModel inputModel, string userId);
    }
}
