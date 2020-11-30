namespace StayFit.Web.ViewModels.Meals
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;

    public class CreateMealInputModel : IMapTo<Meal>
    {
        // [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string PreparationTime { get; set; }

        public string CookingTime { get; set; }

        // [Required]
        [Display(Name = "Skill level")]
        public SkillLevel SkillLevel { get; set; }

        // [Required]
        public string PortionCount { get; set; }

        // [Required]
        [Range(0, 2000)]
        [Display(Name = "Calories")]
        public double KCal { get; set; }

        [Range(0, 200)]
        public double Fat { get; set; }

        public double Saturates { get; set; }

        [Range(0, 200)]
        public double Carbs { get; set; }

        public double Sugars { get; set; }

        public double Fibre { get; set; }

        [Range(0, 200)]
        public double Protein { get; set; }

        public double Salt { get; set; }

        public string Description { get; set; }

        // [Required]
        [Display(Name = "Method of preparation step by step")]
        public string MethodOfPreparation { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        public string SubCategory { get; set; }

        [Required(ErrorMessage = "You need at least one ingredient.")]
        public IEnumerable<MealIngredientInputModel> Ingredients { get; set; }
    }
}
