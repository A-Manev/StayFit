namespace StayFit.Web.ViewModels.Diaries
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class WorkoutDiaryInListViewModel : IMapFrom<WorkoutExercise>
    {
        public string ExerciseName { get; set; }

        public string WorkoutName { get; set; }

        public int Reps { get; set; }

        public int Sets { get; set; }

        public decimal Weight { get; set; }
    }
}
