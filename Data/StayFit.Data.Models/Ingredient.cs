namespace StayFit.Data.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Meals = new HashSet<MealIngredient>();
        }

        public string NameAndQuantity { get; set; }

        public virtual ICollection<MealIngredient> Meals { get; set; }
    }
}
