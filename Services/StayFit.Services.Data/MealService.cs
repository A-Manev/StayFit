namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Meals;

    public class MealService : IMealService
    {
        private readonly IDeletableEntityRepository<Meal> mealRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public MealService(IDeletableEntityRepository<Meal> mealRepository, IDeletableEntityRepository<Ingredient> ingredientRepository, IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.mealRepository = mealRepository;
            this.ingredientRepository = ingredientRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task CreateAsync(CreateMealInputModel inputModel, string userId)
        {
            var subCategoryId = this.subCategoryRepository.AllAsNoTracking().Where(x => x.Name == inputModel.SubCategory).Select(x => x.Id).FirstOrDefault();

            var meal = new Meal
            {
                Name = inputModel.Name,
                PreparationTime = inputModel.PreparationTime,
                CookingTime = inputModel.CookingTime,
                SkillLevel = inputModel.SkillLevel,
                PortionCount = inputModel.PortionCount,
                KCal = inputModel.KCal,
                Fat = inputModel.Fat,
                Saturates = inputModel.Saturates,
                Carbs = inputModel.Carbs,
                Sugars = inputModel.Sugars,
                Fibre = inputModel.Fibre,
                Protein = inputModel.Protein,
                Salt = inputModel.Salt,
                Description = inputModel.Description,
                MethodOfPreparation = inputModel.MethodOfPreparation,
                CategoryId = inputModel.CategoryId,
                SubCategoryId = subCategoryId,
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in inputModel.Ingredients)
            {
                var ingredient = this.ingredientRepository.All().FirstOrDefault(x => x.NameAndQuantity == inputIngredient.NameAndQuantity);

                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        NameAndQuantity = inputIngredient.NameAndQuantity,
                    };
                }

                meal.Ingredients.Add(new MealIngredient
                {
                    Ingredient = ingredient,
                });
            }

            await this.mealRepository.AddAsync(meal);
            await this.mealRepository.SaveChangesAsync();
        }

        public IEnumerable<MealInListViewModel> GetAll(int page, int itemsPerPage = 15)
        {
            return this.mealRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<MealInListViewModel>()
                .ToList();
        }

        public int GetAllMealsCount()
        {
            return this.mealRepository.AllAsNoTracking().Count();
        }
    }
}
