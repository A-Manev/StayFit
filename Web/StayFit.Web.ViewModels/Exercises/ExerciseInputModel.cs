namespace StayFit.Web.ViewModels.Exercises
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ExerciseInputModel
    {
        public string Name { get; set; }

        [Range(0, 100)]
        public int Reps { get; set; }

        [Range(0, 100)]
        public int Sets { get; set; }

        [Range(0, 100)]
        public decimal Weight { get; set; }

        public IEnumerable<ExerciseDropdownViewModel> Exercises { get; set; }
    }
}
