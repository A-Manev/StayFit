namespace StayFit.Web.ViewModels.Users
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class HomePageUserViewModel : IMapFrom<ApplicationUser>
    {
        public double DailyCalories { get; set; }

        public double RemainingCalories { get; set; }

        public double EatenFood => this.DailyCalories - this.RemainingCalories;
    }
}
