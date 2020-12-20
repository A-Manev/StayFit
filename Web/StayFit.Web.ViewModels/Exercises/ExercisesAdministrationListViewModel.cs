namespace StayFit.Web.ViewModels.Exercises
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Pages;

    public class ExercisesAdministrationListViewModel : PagingViewModel
    {
        public IEnumerable<ExercisesAdministrationInListViewModel> Exercises { get; set; }
    }
}
