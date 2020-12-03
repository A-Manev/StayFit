namespace StayFit.Web.ViewModels.Meals
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<MealIngredient>
    {
        public string IngredientNameAndQuantity { get; set; }
    }
}
