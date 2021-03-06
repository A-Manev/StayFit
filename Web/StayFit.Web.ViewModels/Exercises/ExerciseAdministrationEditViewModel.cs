﻿namespace StayFit.Web.ViewModels.Exercises
{
    using System;
    using System.Collections.Generic;

    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;

    public class ExerciseAdministrationEditViewModel : IMapFrom<Exercise>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public BodyPart BodyPart { get; set; }

        public Difficulty Difficulty { get; set; }

        public ExerciseType ExerciseType { get; set; }

        public string Benefits { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int EquipmentId { get; set; }

        public IEnumerable<EquipmentViewModel> Equipments { get; set; }
    }
}
