namespace StayFit.Web.ViewModels.Diaries
{
    using System;
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Users;

    public class FoodDiaryListViewModel
    {
        public DateTime CurrentDate { get; set; }

        public IEnumerable<FoodDiaryInListViewModel> Diary { get; set; }

        public UserCaloriesGoalViewModel User { get; set; }

        public IEnumerable<FoodDiaryInListViewModel> RecentMeals { get; set; }
    }
}
