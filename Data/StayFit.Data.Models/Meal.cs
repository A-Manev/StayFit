namespace StayFit.Data.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Common.Models;
    using StayFit.Data.Models.Enums;

    public class Meal : BaseDeletableModel<int>
    {
        public Meal()
        {
            this.Votes = new HashSet<Vote>();
            this.Images = new HashSet<Image>();
            this.Users = new HashSet<UserMeal>();
            this.Comments = new HashSet<Comment>();
            this.Ingredients = new HashSet<MealIngredient>();
        }

        public string Name { get; set; }

        public string PreparationTime { get; set; }

        public string CookingTime { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public int PortionCount { get; set; }

        public decimal KCal { get; set; }

        public decimal Fat { get; set; }

        public decimal Saturates { get; set; }

        public decimal Carbs { get; set; }

        public decimal Sugars { get; set; }

        public decimal Fibre { get; set; }

        public decimal Protein { get; set; }

        public decimal Salt { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<UserMeal> Users { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<MealIngredient> Ingredients { get; set; }
    }
}
