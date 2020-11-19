namespace StayFit.Data.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Common.Models;
    using StayFit.Data.Models.Enums;

    public class Exercise : BaseDeletableModel<int>
    {
        public Exercise()
        {
            this.Workouts = new HashSet<WorkoutExercise>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public BodyPart BodyPart { get; set; }

        public Difficulty Difficulty { get; set; }

        public ExerciseType ExerciseType { get; set; }

        public string Description { get; set; }

        public string Benefits { get; set; }

        public int EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual ICollection<WorkoutExercise> Workouts { get; set; }
    }
}
