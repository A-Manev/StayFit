namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    public interface IHomeService
    {
        T GetUserInfo<T>(string id);

        Task ChangeUserCalories(string userId);
    }
}
