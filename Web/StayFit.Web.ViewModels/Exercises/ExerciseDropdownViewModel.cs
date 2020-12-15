namespace StayFit.Web.ViewModels.Exercises
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class ExerciseDropdownViewModel : IMapFrom<Exercise>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
