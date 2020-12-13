namespace StayFit.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Meals;

    public class MealService : IMealService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<Meal> mealRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public MealService(IDeletableEntityRepository<Meal> mealRepository, IDeletableEntityRepository<Ingredient> ingredientRepository, IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.mealRepository = mealRepository;
            this.ingredientRepository = ingredientRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task CreateAsync(CreateMealInputModel inputModel, string userId, string imagePath)
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

            Directory.CreateDirectory($"{imagePath}/meals/");

            foreach (var image in inputModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var newImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };

                meal.Images.Add(newImage);
                var physicalPath = $"{imagePath}/meals/{newImage.Id}.{extension}";
                using var fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.mealRepository.AddAsync(meal);
            await this.mealRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 15)
        {
            return this.mealRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public int GetAllMealsCount()
        {
            return this.mealRepository.AllAsNoTracking().Count();
        }

        public T GetMealDetails<T>(int mealId)
        {
            return this.mealRepository.AllAsNoTracking().Where(x => x.Id == mealId)
                .To<T>()
                .FirstOrDefault();
        }

        public (IEnumerable<T> Meals, int Count) GetAllSearched<T>(SearchMealInputModel input, int page, int itemsPerPage = 15)
        {
            var query = this.mealRepository
                .All()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(x => x.Name.Contains(input.Name));
            }

            if (input.CategoryId != 0)
            {
                query = query.Where(x => x.Category.Id == input.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(input.PortionCount))
            {
                query = query.Where(x => x.PortionCount.Contains(input.PortionCount));
            }

            if (!string.IsNullOrWhiteSpace(input.PreparationTime))
            {
                query = query.Where(x => x.PreparationTime.Contains(input.PreparationTime));
            }

            if (!string.IsNullOrWhiteSpace(input.CookingTime))
            {
                query = query.Where(x => x.CookingTime.Contains(input.CookingTime));
            }

            if (input.SkillLevel != 0)
            {
                query = query.Where(x => x.SkillLevel == input.SkillLevel);
            }

            if (!string.IsNullOrWhiteSpace(input.SubCategory) && input.SubCategory != "All")
            {
                query = query.Where(x => x.SubCategory.Name == input.SubCategory);
            }

            return (query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList(), query.ToList().Count);
        }
    }
}
