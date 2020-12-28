namespace StayFit.Services.Data.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StayFit.Data;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Data.Repositories;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Meals;
    using Xunit;

    public class MealServiceTests : BaseServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Meal>> mealRepository;
        private readonly Mock<IDeletableEntityRepository<Ingredient>> ingredientRepository;
        private readonly Mock<IDeletableEntityRepository<SubCategory>> subCategoryRepository;

        public MealServiceTests()
        {
            this.mealRepository = new Mock<IDeletableEntityRepository<Meal>>();
            this.ingredientRepository = new Mock<IDeletableEntityRepository<Ingredient>>();
            this.subCategoryRepository = new Mock<IDeletableEntityRepository<SubCategory>>();
        }

        [Fact]
        public void GetAllMealsCountShouldReturnCorectNumberOfCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Meal>(dbContext);

            var service = new MealService(this.mealRepository.Object, this.ingredientRepository.Object, this.subCategoryRepository.Object);

            var meal = new List<Meal>()
            {
                new Meal
                {
                    AddedByUserId = "userId",
                    Name = "Pancakes",
                    KCal = 100,
                    Protein = 1,
                    Carbs = 2,
                    Fat = 3,
                    Fibre = 4,
                    Sugars = 5,
                    Salt = 6,
                    Saturates = 7,
                    CookingTime = "10 min",
                    PreparationTime = "10 min",
                    PortionCount = "6-8",
                    Category = new Category
                    {
                        Name = "Test",
                        Description = "something for test",
                    },
                },
            };

            this.mealRepository.Setup(m => m.AllAsNoTracking()).Returns(() => meal.AsQueryable());

            var mealCount = service.GetAllMealsCount();

            Assert.Equal(1, mealCount);
        }

        [Fact]
        public async Task CheckCreatingMealAsync()
        {
            ApplicationDbContext db = GetDb();

            var mealRepository = new EfDeletableEntityRepository<Meal>(db);
            var ingredientRepository = new EfDeletableEntityRepository<Ingredient>(db);
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(db);

            var service = new MealService(
               mealRepository, ingredientRepository, subCategoryRepository);

            var list = new List<MealIngredientInputModel>()
            {
                new MealIngredientInputModel { NameAndQuantity = "XD?" },
            };

            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var meal = new CreateMealInputModel
            {
                Name = "TestName",
                PreparationTime = "10 mins",
                CookingTime = "10 mins",
                SkillLevel = SkillLevel.Easy,
                PortionCount = "6-8",
                KCal = 100,
                Fat = 10,
                Saturates = 10,
                Carbs = 10,
                Sugars = 10,
                Fibre = 10,
                Protein = 10,
                Salt = 10,
                Description = "Some text for description",
                MethodOfPreparation = "Some text for methodOfPreparation",
                CategoryId = 1,
                Ingredients = list,
                Images = new List<IFormFile> { file },
            };

            await service.CreateAsync(meal, "userId", "wwwroot/images/meals");

            Assert.Single(db.Meals.Where(x => x.Name == "TestName"));
        }

        [Fact]
        public async Task MethodShouldThrowExceptionUponCreatingMealAsyncWithWrongImageExtension()
        {
            ApplicationDbContext db = GetDb();

            var mealRep = new EfDeletableEntityRepository<Meal>(db);
            var ingredientRep = new EfDeletableEntityRepository<Ingredient>(db);
            var subCategoryRep = new EfDeletableEntityRepository<SubCategory>(db);

            var service = new MealService(
               mealRep, ingredientRep, subCategoryRep);

            var list = new List<MealIngredientInputModel>()
            {
                new MealIngredientInputModel { NameAndQuantity = "XD?" },
            };

            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.exe";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var meal = new CreateMealInputModel
            {
                Name = "TestName",
                PreparationTime = "10 mins",
                CookingTime = "10 mins",
                SkillLevel = SkillLevel.Easy,
                PortionCount = "6-8",
                KCal = 100,
                Fat = 10,
                Saturates = 10,
                Carbs = 10,
                Sugars = 10,
                Fibre = 10,
                Protein = 10,
                Salt = 10,
                Description = "Some text for description",
                MethodOfPreparation = "Some text for methodOfPreparation",
                CategoryId = 1,
                Ingredients = list,
                Images = new List<IFormFile> { file },
            };

            var ex = await Assert.ThrowsAsync<Exception>(async () => await service.CreateAsync(meal, "userId", "wwwroot/images/meals"));

            Assert.Equal("Invalid image extension exe", ex.Message);
        }

        [Fact]
        public void CheckCreatingMealDetailsAsync()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                     Id = 1,
                     Name = "TestName",
                     PreparationTime = "10 mins",
                     CookingTime = "10 mins",
                     SkillLevel = SkillLevel.Easy,
                     PortionCount = "6-8",
                     KCal = 100,
                     Fat = 10,
                     Saturates = 10,
                     Carbs = 10,
                     Sugars = 10,
                     Fibre = 10,
                     Protein = 10,
                     Salt = 10,
                     Description = "Some text for description",
                     MethodOfPreparation = "Some text for methodOfPreparation",
                     CategoryId = 1,
                     SubCategoryId = 1,
                     AddedByUserId = "userId",
                     SubCategory = new SubCategory { Name = "Sub" },
                },
            };

            this.mealRepository.Setup(x => x.AllAsNoTracking())
                .Returns(meals
                .AsQueryable());

            var service = new MealService(
                this.mealRepository.Object,
                this.ingredientRepository.Object,
                this.subCategoryRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(TestMealModel).Assembly);

            var result = service.GetMealDetails<TestMealModel>(1);
            var resultName = service.GetMealDetails<TestMealModel>(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("TestName", resultName.Name);
        }

        [Fact]
        public void GetAllShouldReturnAllMeals()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                     Id = 1,
                     Name = "TestName",
                     PreparationTime = "10 mins",
                     CookingTime = "10 mins",
                     SkillLevel = SkillLevel.Easy,
                     PortionCount = "6-8",
                     KCal = 100,
                     Fat = 10,
                     Saturates = 10,
                     Carbs = 10,
                     Sugars = 10,
                     Fibre = 10,
                     Protein = 10,
                     Salt = 10,
                     Description = "Some text for description",
                     MethodOfPreparation = "Some text for methodOfPreparation",
                     CategoryId = 1,
                     SubCategoryId = 1,
                     AddedByUserId = "userId",
                     SubCategory = new SubCategory { Name = "Sub" },
                },
                new Meal
                {
                     Id = 2,
                     Name = "TestName",
                     PreparationTime = "10 mins",
                     CookingTime = "10 mins",
                     SkillLevel = SkillLevel.Easy,
                     PortionCount = "6-8",
                     KCal = 100,
                     Fat = 10,
                     Saturates = 10,
                     Carbs = 10,
                     Sugars = 10,
                     Fibre = 10,
                     Protein = 10,
                     Salt = 10,
                     Description = "Some text for description",
                     MethodOfPreparation = "Some text for methodOfPreparation",
                     CategoryId = 1,
                     SubCategoryId = 1,
                     AddedByUserId = "userId",
                     SubCategory = new SubCategory { Name = "Sub" },
                },
            };

            this.mealRepository.Setup(x => x.AllAsNoTracking())
               .Returns(meals
               .AsQueryable());

            var service = new MealService(
                this.mealRepository.Object,
                this.ingredientRepository.Object,
                this.subCategoryRepository.Object);

            var result = service.GetAll<TestMealModel>(1);

            AutoMapperConfig.RegisterMappings(typeof(TestMealModel).Assembly);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetRandomShouldReturnAllMeals()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                     Id = 1,
                     Name = "TestName",
                     PreparationTime = "10 mins",
                     CookingTime = "10 mins",
                     SkillLevel = SkillLevel.Easy,
                     PortionCount = "6-8",
                     KCal = 100,
                     Fat = 10,
                     Saturates = 10,
                     Carbs = 10,
                     Sugars = 10,
                     Fibre = 10,
                     Protein = 10,
                     Salt = 10,
                     Description = "Some text for description",
                     MethodOfPreparation = "Some text for methodOfPreparation",
                     CategoryId = 1,
                     SubCategoryId = 1,
                     AddedByUserId = "userId",
                     SubCategory = new SubCategory { Name = "Sub" },
                },
                new Meal
                {
                     Id = 2,
                     Name = "TestName",
                     PreparationTime = "10 mins",
                     CookingTime = "10 mins",
                     SkillLevel = SkillLevel.Easy,
                     PortionCount = "6-8",
                     KCal = 100,
                     Fat = 10,
                     Saturates = 10,
                     Carbs = 10,
                     Sugars = 10,
                     Fibre = 10,
                     Protein = 10,
                     Salt = 10,
                     Description = "Some text for description",
                     MethodOfPreparation = "Some text for methodOfPreparation",
                     CategoryId = 1,
                     SubCategoryId = 1,
                     AddedByUserId = "userId",
                     SubCategory = new SubCategory { Name = "Sub" },
                },
            };

            this.mealRepository.Setup(x => x.AllAsNoTracking())
               .Returns(meals
               .AsQueryable());

            var service = new MealService(
                this.mealRepository.Object,
                this.ingredientRepository.Object,
                this.subCategoryRepository.Object);

            var result = service.GetRandom<TestMealModel>(2);

            AutoMapperConfig.RegisterMappings(typeof(TestMealModel).Assembly);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetRandomShouldReturnAllMealsExeption()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                },
            };

            this.mealRepository.Setup(x => x.AllAsNoTracking())
               .Returns(meals
               .AsQueryable());

            var service = new MealService(
                this.mealRepository.Object,
                this.ingredientRepository.Object,
                this.subCategoryRepository.Object);

            Assert.Throws<NullReferenceException>(() => service.GetRandom<TestMealModel>(3));
        }

        [Fact]
        public void GetAllSearchedShouldReturnAllSearchedMeals()
        {
            var meals = new List<Meal>
            {
                new Meal
                {
                     Id = 1,
                     Name = "TestName",
                     PreparationTime = "10 mins",
                     CookingTime = "10 mins",
                     SkillLevel = SkillLevel.Easy,
                     PortionCount = "6-8",
                     KCal = 100,
                     Fat = 10,
                     Saturates = 10,
                     Carbs = 10,
                     Sugars = 10,
                     Fibre = 10,
                     Protein = 10,
                     Salt = 10,
                     Description = "Some text for description",
                     MethodOfPreparation = "Some text for methodOfPreparation",
                     CategoryId = 1,
                     SubCategoryId = 1,
                     SubCategory = new SubCategory { Name = "Sub" },
                     AddedByUserId = "userId",
                     Category = new Category { Id = 1, Name = "category" },
                },
            };

            this.mealRepository.Setup(x => x.All())
                .Returns(meals
                .AsQueryable());

            var service = new MealService(
                this.mealRepository.Object,
                this.ingredientRepository.Object,
                this.subCategoryRepository.Object);

            var inputModel = new SearchMealInputModel
            {
                Name = "TestName",
                PortionCount = "6-8",
                PreparationTime = "10 mins",
                CookingTime = "10 mins",
                SkillLevel = SkillLevel.Easy,
                SubCategory = "Sub",
                CategoryId = 1,
            };

            AutoMapperConfig.RegisterMappings(typeof(TestMealModel).Assembly);

            var result = service.GetAllSearched<TestMealModel>(inputModel, 1);

            Assert.Equal(1, result.Count);
            Assert.Equal("TestName", result.Meals.FirstOrDefault().Name);
            Assert.Equal("6-8", result.Meals.FirstOrDefault().PortionCount);
            Assert.Equal("10 mins", result.Meals.FirstOrDefault().PreparationTime);
            Assert.Equal("10 mins", result.Meals.FirstOrDefault().CookingTime);
            Assert.Equal(SkillLevel.Easy, result.Meals.FirstOrDefault().SkillLevel);
            Assert.Equal("Sub", result.Meals.FirstOrDefault().SubCategoryName);
            Assert.Equal(1, result.Meals.FirstOrDefault().CategoryId);
        }

        public class TestMealModel : IMapFrom<Meal>
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int CategoryId { get; set; }

            public string SubCategoryName { get; set; }

            public SkillLevel SkillLevel { get; set; }

            public string PortionCount { get; set; }

            public string PreparationTime { get; set; }

            public string CookingTime { get; set; }
        }
    }
}
