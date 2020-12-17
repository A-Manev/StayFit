namespace StayFit.Web.ViewModels.Exercises
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class EquipmentViewModel : IMapFrom<Equipment>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
