namespace StayFit.Data.Models
{
    using StayFit.Data.Common.Models;

    public class MealDiary : BaseDeletableModel<int>
    {
        public int MealId { get; set; }

        public virtual Meal Meal { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public decimal MealQuantity { get; set; }
    }
}
