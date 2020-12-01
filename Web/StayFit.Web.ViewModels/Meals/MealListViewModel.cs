namespace StayFit.Web.ViewModels.Meals
{
    using System.Collections.Generic;

    public class MealListViewModel
    {
        public IEnumerable<MealInListViewModel> Meals { get; set; }

        public int PageNumber { get; set; }

        public int MealsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
