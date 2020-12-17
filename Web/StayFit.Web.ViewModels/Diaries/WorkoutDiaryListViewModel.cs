namespace StayFit.Web.ViewModels.Diaries
{
    using System;
    using System.Collections.Generic;

    public class WorkoutDiaryListViewModel
    {
        public DateTime CurrentDate { get; set; }

        public IEnumerable<WorkoutDiaryInListViewModel> Diary { get; set; }
    }
}
