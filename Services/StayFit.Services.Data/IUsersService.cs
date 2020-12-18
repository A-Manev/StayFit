namespace StayFit.Services.Data
{
    using System.Threading.Tasks;

    using StayFit.Data.Models;
    using StayFit.Web.ViewModels.Users;

    public interface IUsersService
    {
        double CalculateUserCalories(ApplicationUser user);

        T GetById<T>(string userId);

        Task UploadImageAsync(HomePageUserViewModel inputModel, string userId, string imagePath);
    }
}
