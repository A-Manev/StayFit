namespace StayFit.Services.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Models.Enums;

    public class MealDto
    {
        public MealDto()
        {
            this.Ingredients = new List<string>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string PreparationTime { get; set; }

        public string CookingTime { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public string PortionCount { get; set; }

        public decimal KCal { get; set; }

        public decimal Fat { get; set; }

        public decimal Saturates { get; set; }

        public decimal Carbs { get; set; }

        public decimal Sugars { get; set; }

        public decimal Fibre { get; set; }

        public decimal Protein { get; set; }

        public decimal Salt { get; set; }

        public string Description { get; set; }

        public string MethodOfPreparation { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public string SubCategoryName { get; set; }

        public string SubCategoryDescription { get; set; }

        public List<string> Ingredients { get; set; }
    }
}
