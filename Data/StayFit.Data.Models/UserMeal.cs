namespace StayFit.Data.Models
{
    public class UserMeal
    {
        public int Id { get; set; }

        public int MealId { get; set; }

        public virtual Meal Meal { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public decimal MealQuantity { get; set; }
    }
}
