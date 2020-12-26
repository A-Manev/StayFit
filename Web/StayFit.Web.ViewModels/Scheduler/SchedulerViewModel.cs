namespace StayFit.Web.ViewModels.Scheduler
{
    using System.Collections.Generic;

    public class SchedulerViewModel
    {
        public IEnumerable<CalendarEvent> Events { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
