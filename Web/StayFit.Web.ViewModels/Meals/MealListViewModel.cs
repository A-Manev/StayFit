namespace StayFit.Web.ViewModels.Meals
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Pages;

    public class MealListViewModel : PagingViewModel
    {
        public IEnumerable<MealInListViewModel> Meals { get; set; }
    }
}
