namespace StayFit.Data.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }

        public int Reps { get; set; }

        public int Sets { get; set; }

        public string Note { get; set; }

        public decimal Weight { get; set; }
    }
}
