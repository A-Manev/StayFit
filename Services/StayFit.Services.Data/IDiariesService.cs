namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface IDiariesService
    {
        Task AddMealToDiary(int mealId, string userId, double quantity);
    }
}
