namespace StayFit.Web.ViewModels.Exercises
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Pages;

    public class ExercisesListViewModel : PagingViewModel
    {
        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}
