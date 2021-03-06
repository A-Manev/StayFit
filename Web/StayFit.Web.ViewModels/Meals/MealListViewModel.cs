﻿namespace StayFit.Web.ViewModels.Meals
{
    using System.Collections.Generic;

    using StayFit.Data.Models.Enums;
    using StayFit.Web.ViewModels.Pages;

    public class MealListViewModel : PagingViewModel
    {
        public IEnumerable<MealInListViewModel> Meals { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string SubCategory { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public string PortionCount { get; set; }

        public string PreparationTime { get; set; }

        public string CookingTime { get; set; }
    }
}
