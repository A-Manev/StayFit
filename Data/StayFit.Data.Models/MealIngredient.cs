namespace StayFit.Data.Models
{
    public class MealIngredient
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public virtual Meal Meal { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
