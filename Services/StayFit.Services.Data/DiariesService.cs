namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Diaries;

    public class DiariesService : IDiariesService
    {
        private readonly IDeletableEntityRepository<MealDiary> mealsDiaryRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Meal> mealRepository;

        public DiariesService(IDeletableEntityRepository<MealDiary> mealsDiaryRepository, IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<Meal> mealRepository)
        {
            this.mealsDiaryRepository = mealsDiaryRepository;
            this.userRepository = userRepository;
            this.mealRepository = mealRepository;
        }

        public async Task AddMealToDiaryAsync(int mealId, string userId, double quantity = 1)
        {
            var user = this.userRepository.All().Where(x => x.Id == userId).FirstOrDefault();
            var meal = this.mealRepository.AllAsNoTracking().Where(x => x.Id == mealId).FirstOrDefault();

            user.RemainingCalories -= meal.KCal * quantity;

            var mealDiary = new MealDiary
            {
                MealId = mealId,
                UserId = userId,
                MealQuantity = quantity,
            };

            await this.mealsDiaryRepository.AddAsync(mealDiary);
            await this.mealsDiaryRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }

        public IEnumerable<FoodDiaryViewModel> GetUserFoodDiary(string userId)
        {
            return this.mealsDiaryRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<FoodDiaryViewModel>()
                .ToList();

            //return this.mealsDiaryRepository.All()
            //    .Where(x => x.UserId == userId)
            //    .Select(x => new MealDiaryViewModel
            //    {
            //        Id = x.Meal.Id,
            //        Name = x.Meal.Name,
            //        KCal = x.Meal.KCal,
            //        Protein = x.Meal.Protein,
            //        Fat = x.Meal.Fat,
            //        Carbs = x.Meal.Carbs,
            //    })
            //    .ToList();
        }
    }
}
