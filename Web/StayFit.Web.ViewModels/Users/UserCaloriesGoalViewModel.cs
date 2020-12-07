namespace StayFit.Web.ViewModels.Users
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class UserCaloriesGoalViewModel : IMapFrom<ApplicationUser>
    {
        public double DailyCalories { get; set; }
    }
}
