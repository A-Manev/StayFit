namespace StayFit.Web.ViewModels.Exercises
{
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;

    public class ExerciseViewModel : IMapFrom<Exercise>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public BodyPart BodyPart { get; set; }

        public string EquipmentName { get; set; }
    }
}
