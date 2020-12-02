namespace StayFit.Web.ViewModels.Meals
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;

    public class CreateMealInputModel : IMapTo<Meal>
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string PreparationTime { get; set; }

        [Required]
        public string CookingTime { get; set; }

        [Required(ErrorMessage = "Add at least one image.")]
        public IEnumerable<IFormFile> Images { get; set; }

        [Required]
        [Display(Name = "Skill level")]
        public SkillLevel SkillLevel { get; set; }

        [Required]
        public string PortionCount { get; set; }

        [Required]
        [Range(0, 2000)]
        [Display(Name = "Calories(g)")]
        public double KCal { get; set; }

        [Range(0, 200)]
        [Display(Name = "Fat(g)")]
        public double Fat { get; set; }

        [Display(Name = "Saturates(g)")]
        public double Saturates { get; set; }

        [Range(0, 200)]
        [Display(Name = "Carbs(g)")]
        public double Carbs { get; set; }

        [Display(Name = "Sugars(g)")]
        public double Sugars { get; set; }

        [Display(Name = "Fibre(g)")]
        public double Fibre { get; set; }

        [Range(0, 200)]
        [Display(Name = "Protein(g)")]
        public double Protein { get; set; }

        [Display(Name = "Salt(g)")]
        public double Salt { get; set; }

        public string Description { get; set; }

        [Required]
        [MinLength(100)]
        [Display(Name = "Method of preparation step by step")]
        public string MethodOfPreparation { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Subcategory")]
        public string SubCategory { get; set; }

        [Required(ErrorMessage = "You need at least one ingredient.")]
        public IEnumerable<MealIngredientInputModel> Ingredients { get; set; }
    }
}
