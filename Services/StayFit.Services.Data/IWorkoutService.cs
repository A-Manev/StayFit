namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Exercises;

    public interface IWorkoutService
    {
        Task CreateAsync(List<ExerciseInputModel> exercises, string userId);
    }
}
