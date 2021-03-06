﻿namespace StayFit.Web.ViewModels.Exercises
{
    using System.Collections.Generic;

    using StayFit.Data.Models.Enums;
    using StayFit.Web.ViewModels.Pages;

    public class ExercisesListViewModel : PagingViewModel
    {
        public IEnumerable<ExerciseViewModel> Exercises { get; set; }

        public string Name { get; set; }

        public BodyPart BodyPart { get; set; }

        public Difficulty Difficulty { get; set; }

        public ExerciseType ExerciseType { get; set; }

        public int EquipmentId { get; set; }
    }
}
