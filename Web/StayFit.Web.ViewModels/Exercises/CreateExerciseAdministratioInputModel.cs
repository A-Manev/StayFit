namespace StayFit.Web.ViewModels.Exercises
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using StayFit.Data.Models.Enums;

    public class CreateExerciseAdministratioInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Body Part")]
        public BodyPart BodyPart { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        [Display(Name = "Exercise Type")]
        public ExerciseType ExerciseType { get; set; }

        public string Description { get; set; }

        public string Benefits { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        public IEnumerable<EquipmentViewModel> Equipments { get; set; }
    }
}
