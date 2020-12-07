namespace StayFit.Web.ViewModels.Diaries
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class FoodDiaryInListViewModel : IMapFrom<MealDiary>
    {
        public int Id { get; set; }

        public double MealQuantity { get; set; }

        public string MealName { get; set; }

        public double MealProtein { get; set; }

        public double MealCarbs { get; set; }

        public double MealFat { get; set; }

        public double MealKCal { get; set; }
    }
}
