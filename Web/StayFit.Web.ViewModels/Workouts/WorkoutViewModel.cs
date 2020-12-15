namespace StayFit.Web.ViewModels.Workouts
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class WorkoutViewModel : IMapFrom<Workout>
    {
        public string ExerciseName { get; set; }

        public int Reps { get; set; }

        public int Sets { get; set; }

        public decimal Weight { get; set; }
    }
}
