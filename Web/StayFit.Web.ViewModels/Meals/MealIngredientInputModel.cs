namespace StayFit.Web.ViewModels.Meals
{
    using System.ComponentModel.DataAnnotations;

    public class MealIngredientInputModel
    {
        [Required(ErrorMessage = "Ingredient should have quantity and name.")]
        [MinLength(3)]
        [Display(Name = "Quantity and name")]
        public string NameAndQuantity { get; set; }
    }
}
