namespace StayFit.Web.ViewModels.Meals
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using StayFit.Data.Models.Enums;
    using StayFit.Web.ViewModels.Categories;

    public class SearchMealInputModel
    {
        public string Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Subcategory")]
        public string SubCategory { get; set; }

        [Display(Name = "Skill level")]
        public SkillLevel SkillLevel { get; set; }

        [Display(Name = "Portion Count")]
        public string PortionCount { get; set; }

        [Display(Name = "Preparation Time")]
        public string PreparationTime { get; set; }

        [Display(Name = "Cooking Time")]
        public string CookingTime { get; set; }

        public IEnumerable<CategoryViewModel> CategoriesItems { get; set; }
    }
}
