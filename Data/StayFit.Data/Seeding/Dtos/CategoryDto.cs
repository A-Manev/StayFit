namespace StayFit.Data.Seeding.Dtos
{
    using System.Collections.Generic;

    public class CategoryDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<SubCategoryDto> SubCategories { get; set; }
    }
}
