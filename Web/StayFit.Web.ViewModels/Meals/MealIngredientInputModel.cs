namespace StayFit.Web.ViewModels.Meals
{
    using System.ComponentModel.DataAnnotations;

    public class MealIngredientInputModel
    {
        // [Required]
        // [MinLength(5)]
        // [Display(Name = "Quantity and name")]
        public string NameAndQuantity { get; set; }
    }
}
