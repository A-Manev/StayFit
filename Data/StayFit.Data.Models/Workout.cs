namespace StayFit.Data.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Common.Models;

    public class Workout : BaseDeletableModel<int>
    {
        public Workout()
        {
            this.Exercises = new HashSet<WorkoutExercise>();
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<WorkoutExercise> Exercises { get; set; }
    }
}
