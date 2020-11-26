namespace StayFit.Services.Data
{
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;

    public class UsersService : IUsersService
    {
        public double CalculateUserCalories(ApplicationUser user)
        {
            double totalCalories = 0.0;

            if (user.Gender == Gender.Male && user.ActivityLevel == ActivityLevel.Sedentary)
            {
                totalCalories = (1.2 * (66.5 + (13.75 * user.CurrentWeight))) + (5.003 * user.Height) - (6.755 * user.Age);
            }
            else if (user.Gender == Gender.Male && user.ActivityLevel == ActivityLevel.LightlyActive)
            {
                totalCalories = (1.375 * (66.5 + (13.75 * user.CurrentWeight))) + (5.003 * user.Height) - (6.755 * user.Age);
            }
            else if (user.Gender == Gender.Male && user.ActivityLevel == ActivityLevel.ModeratelyActive)
            {
                totalCalories = (1.55 * (66.5 + (13.75 * user.CurrentWeight))) + (5.003 * user.Height) - (6.755 * user.Age);
            }
            else if (user.Gender == Gender.Male && user.ActivityLevel == ActivityLevel.VeryActive)
            {
                totalCalories = (1.725 * (66.5 + (13.75 * user.CurrentWeight))) + (5.003 * user.Height) - (6.755 * user.Age);
            }
            else if (user.Gender == Gender.Male && user.ActivityLevel == ActivityLevel.ExtraActive)
            {
                totalCalories = (1.9 * (66.5 + (13.75 * user.CurrentWeight))) + (5.003 * user.Height) - (6.755 * user.Age);
            }
            else if (user.Gender == Gender.Female && user.ActivityLevel == ActivityLevel.Sedentary)
            {
                totalCalories = (1.2 * (665 + (9.563 * user.CurrentWeight))) + (1.850 * user.Height) - (4.676 * user.Age);
            }
            else if (user.Gender == Gender.Female && user.ActivityLevel == ActivityLevel.LightlyActive)
            {
                totalCalories = (1.375 * (665 + (9.563 * user.CurrentWeight))) + (1.850 * user.Height) - (4.676 * user.Age);
            }
            else if (user.Gender == Gender.Female && user.ActivityLevel == ActivityLevel.ModeratelyActive)
            {
                totalCalories = (1.55 * (665 + (9.563 * user.CurrentWeight))) + (1.850 * user.Height) - (4.676 * user.Age);
            }
            else if (user.Gender == Gender.Female && user.ActivityLevel == ActivityLevel.VeryActive)
            {
                totalCalories = (1.725 * (665 + (9.563 * user.CurrentWeight))) + (1.850 * user.Height) - (4.676 * user.Age);
            }
            else if (user.Gender == Gender.Female && user.ActivityLevel == ActivityLevel.ExtraActive)
            {
                totalCalories = (1.9 * (665 + (9.563 * user.CurrentWeight))) + (1.850 * user.Height) - (4.676 * user.Age);
            }

            return totalCalories;
        }
    }
}
