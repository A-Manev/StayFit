namespace StayFit.Web.ViewModels.Exercises
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using StayFit.Data.Models.Enums;

    public class SearchExerciseInputModel
    {
        public string Name { get; set; }

        [Display(Name = "Body Part")]
        public BodyPart BodyPart { get; set; }

        public Difficulty Difficulty { get; set; }

        [Display(Name = "Exercise Type")]
        public ExerciseType ExerciseType { get; set; }

        [Display(Name = "Equipment")]
        public int EquipmentId { get; set; }

        public IEnumerable<EquipmentViewModel> Equipments { get; set; }
    }
}
