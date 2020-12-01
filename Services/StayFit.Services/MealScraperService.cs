namespace StayFit.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Models;

    public class MealScraperService : IMealScraperService
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        private readonly IDeletableEntityRepository<Meal> mealRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;
        private readonly IRepository<MealIngredient> mealIngredientRepository;
        private readonly IRepository<Image> imageRepository;

        public MealScraperService(IDeletableEntityRepository<Meal> mealRepository, IDeletableEntityRepository<Category> categoryRepository, IDeletableEntityRepository<SubCategory> subCategoryRepository, IDeletableEntityRepository<Ingredient> ingredientRepository, IRepository<MealIngredient> mealIngredientRepository, IRepository<Image> imageRepository)
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);

            this.mealRepository = mealRepository;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.ingredientRepository = ingredientRepository;
            this.mealIngredientRepository = mealIngredientRepository;
            this.imageRepository = imageRepository;
        }

        public async Task PopulateDbWithMeal(int pagesCount)
        {
            var mealBag = new ConcurrentBag<MealDto>();

            List<string> allCategories = new List<string>
            {
                "christmas",
                "everyday",
                "quick-easy",
                "cakes-baking",
                "seasonal",
                "dishe",
                "dinner-ideas",
                "budget",
                "cocktails-drinks",
            };

            foreach (var category in allCategories)
            {
                string link = "https://www.bbcgoodfood.com/recipes/category/all-" + category;

                Parallel.For(1, pagesCount, (pageNumber) =>
                {
                    try
                    {
                        var allMeal = this.GetCategoryWithAllSubCategories(link, pageNumber);

                        Parallel.ForEach(allMeal, (meal) =>
                        {
                            mealBag.Add(meal);
                        });
                    }
                    catch (Exception)
                    {
                    }
                });

                foreach (var meal in mealBag)
                {
                    var categoryId = await this.GetOrCreateCategoryAsync(meal.CategoryName, meal.CategoryDescription);
                    var subCategoryId = await this.GetOrCreateSubCategoryAsync(meal.SubCategoryName, meal.SubCategoryDescription);
                    var mealExist = this.mealRepository.AllAsNoTracking().Any(x => x.Name == meal.Name);

                    if (mealExist)
                    {
                        continue;
                    }

                    var newMeal = new Meal
                    {
                        Name = meal.Name,
                        ImageUrl = meal.ImageUrl,
                        PreparationTime = meal.PreparationTime,
                        CookingTime = meal.CookingTime,
                        SkillLevel = meal.SkillLevel,
                        PortionCount = meal.PortionCount,
                        KCal = meal.KCal,
                        Fat = meal.Fat,
                        Saturates = meal.Saturates,
                        Carbs = meal.Carbs,
                        Sugars = meal.Sugars,
                        Fibre = meal.Fibre,
                        Protein = meal.Protein,
                        Salt = meal.Salt,
                        Description = meal.Description,
                        MethodOfPreparation = meal.MethodOfPreparation,
                        CategoryId = categoryId,
                        SubCategoryId = subCategoryId,
                    };

                    await this.mealRepository.AddAsync(newMeal);
                    await this.mealRepository.SaveChangesAsync();

                    foreach (var ingredientNameAndQuantity in meal.Ingredients)
                    {
                        var ingridientId = await this.GetOrCreateIngredientAsync(ingredientNameAndQuantity);

                        var mealIngredient = new MealIngredient
                        {
                            IngredientId = ingridientId,
                            MealId = newMeal.Id,
                        };

                        await this.mealIngredientRepository.AddAsync(mealIngredient);
                        await this.mealIngredientRepository.SaveChangesAsync();
                    }

                    var image = new Image
                    {
                        RemoteImageUrl = meal.ImageUrl,
                        MealId = newMeal.Id,
                    };

                    await this.imageRepository.AddAsync(image);
                    await this.imageRepository.SaveChangesAsync();
                }
            }
        }

        private async Task<int> GetOrCreateIngredientAsync(string ingredientNameAndQuantity)
        {
            var ingredient = this.ingredientRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.NameAndQuantity == ingredientNameAndQuantity);

            if (ingredient == null)
            {
                ingredient = new Ingredient
                {
                    NameAndQuantity = ingredientNameAndQuantity,
                };

                await this.ingredientRepository.AddAsync(ingredient);
                await this.ingredientRepository.SaveChangesAsync();
            }

            return ingredient.Id;
        }

        private async Task<int> GetOrCreateSubCategoryAsync(string subCategoryName, string subCategoryDescription)
        {
            var subCategory = this.subCategoryRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == subCategoryName);

            if (subCategory == null)
            {
                subCategory = new SubCategory()
                {
                    Name = subCategoryName,
                    Description = subCategoryDescription,
                };

                await this.subCategoryRepository.AddAsync(subCategory);
                await this.subCategoryRepository.SaveChangesAsync();
            }

            return subCategory.Id;
        }

        private async Task<int> GetOrCreateCategoryAsync(string categoryName, string categoryDescription)
        {
            var category = this.categoryRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == categoryName);

            if (category == null)
            {
                category = new Category()
                {
                    Name = categoryName,
                    Description = categoryDescription,
                };

                await this.categoryRepository.AddAsync(category);
                await this.categoryRepository.SaveChangesAsync();
            }

            return category.Id;
        }

        private ConcurrentBag<MealDto> GetCategoryWithAllSubCategories(string categoryLink, int pageNumber)
        {
            var bag = new ConcurrentBag<MealDto>();

            var page = this.context.OpenAsync(categoryLink + "/" + pageNumber).GetAwaiter().GetResult();

            var categoryName = page.QuerySelector("div > h1");

            var categoryDescription = page.QuerySelector("div > p");

            var allSubCategories = page.QuerySelectorAll("div > div > div.standard-card-new__main-info > div > h4 > a");

            Parallel.ForEach(allSubCategories, (subCategory) =>
            {
                var url = subCategory.OuterHtml
                    .ToString()
                    .Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var subCategoryUrl = url[3].Replace('"', ' ').Trim() + "/";

                int pagesCount = 3;

                Parallel.For(1, pagesCount, (currentPageNumber) =>
                {
                    try
                    {
                        var meals = this.GetSubCategoryWithAllMeal(subCategoryUrl, currentPageNumber);

                        Parallel.ForEach(meals, (meal) =>
                        {
                            meal.CategoryName = categoryName.TextContent.Trim();
                            meal.CategoryDescription = categoryDescription.TextContent.Trim();

                            bag.Add(meal);
                        });
                    }
                    catch (Exception)
                    {
                    }
                });
            });

            return bag;
        }

        private ConcurrentBag<MealDto> GetSubCategoryWithAllMeal(string subCategoryUrl, int pageNumber)
        {
            var bag = new ConcurrentBag<MealDto>();

            var page = this.context.OpenAsync(subCategoryUrl + pageNumber).GetAwaiter().GetResult();

            var subCategoryName = page.QuerySelector("div.template-article__header-main.template-article__header-main--masthead-led > h1");

            var subCategoryDescription = page.QuerySelector("div.template-article__header-main.template-article__header-main--masthead-led > p");

            var allMealOnCurrnetSubCategoryPage = page.QuerySelectorAll("div.standard-card-new__main-info > div > h4 > a");

            Parallel.ForEach(allMealOnCurrnetSubCategoryPage, (currentMeal) =>
            {
                var url = currentMeal.OuterHtml
                   .ToString()
                   .Split(new string[] { ".com", "\">" }, StringSplitOptions.RemoveEmptyEntries)
                   .ToArray();

                var mealUrl = "https://www.bbcgoodfood.com/recipes" + url[1];

                var meal = this.GetMeal(mealUrl);

                meal.SubCategoryName = subCategoryName.TextContent.Trim();
                meal.SubCategoryDescription = subCategoryDescription.TextContent.Trim();

                bag.Add(meal);
            });

            return bag;
        }

        private MealDto GetMeal(string exerciseUrl)
        {
            var document = this.context.OpenAsync(exerciseUrl).GetAwaiter().GetResult();

            var meal = new MealDto();

            var mealName = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > h1");

            meal.Name = mealName.TextContent.Trim();

            var imageUrl = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__image > div > div > picture > img").GetAttribute("src");

            meal.ImageUrl = imageUrl;

            var kcal = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(3) > tr:nth-child(1) > td.key-value-blocks__value");
            var fat = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(3) > tr:nth-child(2) > td.key-value-blocks__value");
            var saturates = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(3) > tr:nth-child(3) > td.key-value-blocks__value");
            var carbs = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(3) > tr:nth-child(4) > td.key-value-blocks__value");
            var sugars = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(4) > tr:nth-child(1) > td.key-value-blocks__value");
            var fibre = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(4) > tr:nth-child(2) > td.key-value-blocks__value");
            var protein = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(4) > tr:nth-child(3) > td.key-value-blocks__value");
            var salt = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > table > tbody:nth-child(4) > tr:nth-child(4) > td.key-value-blocks__value");

            meal.KCal = double.Parse(kcal.TextContent.Trim().Replace("g", string.Empty));
            meal.Fat = double.Parse(fat.TextContent.Trim().Replace("g", string.Empty));
            meal.Saturates = double.Parse(saturates.TextContent.Trim().Replace("g", string.Empty));
            meal.Carbs = double.Parse(carbs.TextContent.Trim().Replace("g", string.Empty));
            meal.Sugars = double.Parse(sugars.TextContent.Trim().Replace("g", string.Empty));
            meal.Fibre = double.Parse(fibre.TextContent.Trim().Replace("g", string.Empty));
            meal.Protein = double.Parse(protein.TextContent.Trim().Replace("g", string.Empty));
            meal.Salt = double.Parse(salt.TextContent.Trim().Replace("g", string.Empty));

            var preparationTime = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > ul.masthead__row.masthead__planning.mb-xxs.list.list--horizontal > li:nth-child(1) > div > div.icon-with-text__children > ul > li:nth-child(1) > span:nth-child(2) > time");

            meal.PreparationTime = preparationTime.TextContent.Trim();

            var cookingTime = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > ul.masthead__row.masthead__planning.mb-xxs.list.list--horizontal > li:nth-child(1) > div > div.icon-with-text__children > ul > li:nth-child(2) > span:nth-child(2) > time");

            // 1 mins
            // 10 mins
            // 1 hr
            // 2 hrs
            // 1 hr and 1 min
            // 1 hr and 10 mins
            // 2 hrs and 1 min
            // 2 hrs and 10 mins
            meal.CookingTime = cookingTime.TextContent.Trim();

            var portionsCount = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > ul.masthead__row.masthead__planning.mb-xxs.list.list--horizontal > li:nth-child(3) > div > div.icon-with-text__children");

            meal.PortionCount = portionsCount.TextContent.Trim();

            var skillLevel = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > ul.masthead__row.masthead__planning.mb-xxs.list.list--horizontal > li:nth-child(2) > div > div.icon-with-text__children");

            // Easy
            // More effort
            // A challenge
            var skillLevelToEnums = skillLevel.TextContent.Trim();

            if (skillLevelToEnums == "Easy")
            {
                meal.SkillLevel = SkillLevel.Easy;
            }
            else if (skillLevelToEnums == "More effort")
            {
                meal.SkillLevel = SkillLevel.MoreEffort;
            }
            else if (skillLevelToEnums == "A challenge")
            {
                meal.SkillLevel = SkillLevel.AChallenge;
            }

            var description = document.QuerySelector("#__next > div.default-layout > main > div.recipe-template > section > div > div.masthead__body > div.mb-lg > div > p");

            meal.Description = description.TextContent.Trim();

            var ingredients = document.QuerySelectorAll("#__next > div.default-layout > main > div.recipe-template > div > div > div.layout-md-rail__primary > div.recipe-template__content > div.row.recipe-template__instructions > section.recipe-template__ingredients.col-12.mt-md.col-lg-6 > section > ul > li");

            foreach (var ingredient in ingredients)
            {
                meal.Ingredients.Add(ingredient.TextContent.Trim());
            }

            var methods = document.QuerySelectorAll("#__next > div.default-layout > main > div.recipe-template > div > div > div.layout-md-rail__primary > div.recipe-template__content > div.row.recipe-template__instructions > section.recipe-template__method-steps.mb-lg.col-12.col-lg-6 > div > ul > li > div")
                .Select(x => x.TextContent)
                .ToList();

            var methodOfPreparation = new StringBuilder();

            foreach (var item in methods)
            {
                methodOfPreparation.AppendLine(item);
            }

            meal.MethodOfPreparation = methodOfPreparation.ToString().TrimEnd();

            return meal;
        }
    }
}
