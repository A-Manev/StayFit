namespace StayFit.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using StayFit.Data.Models;
    using StayFit.Data.Seeding.Dtos;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            StreamReader streamReader = new StreamReader("wwwroot\\categories.json");

            string jsonString = await streamReader.ReadToEndAsync();

            var categoryDtos = JsonConvert.DeserializeObject<List<CategoryDto>>(jsonString);

            var categories = new List<Category>();

            var subCategories = new List<SubCategory>();

            foreach (var categoryDto in categoryDtos)
            {
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                };

                categories.Add(category);

                foreach (var subCategoryDto in categoryDto.SubCategories)
                {
                    var subCategory = new SubCategory
                    {
                        Name = subCategoryDto.Name,
                        Description = subCategoryDto.Description,
                    };

                    subCategories.Add(subCategory);
                }
            }

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SubCategories.AddRangeAsync(subCategories);
        }
    }
}
