namespace StayFit.Web.ViewModels.Users
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class UserCaloriesGoalViewModel : IMapFrom<ApplicationUser>
    {
        public double DailyCalories { get; set; }

        public double Protein { get; set; }

        public double Carbs { get; set; }

        public double Fat { get; set; }
    }
}
