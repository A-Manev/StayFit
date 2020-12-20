namespace StayFit.Web.ViewModels.Exercises
{
    using System;

    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class ExercisesAdministrationInListViewModel : IMapFrom<Exercise>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
