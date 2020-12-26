namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Exercises;
    using StayFit.Web.ViewModels.Scheduler;

    public interface IWorkoutService
    {
        Task CreateAsync(List<ExerciseInputModel> exercises, string userId);

        List<CalendarEvent> GetEvents(string userId);
    }
}
