namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return this.categoryRepository
                .AllAsNoTracking()
                .To<CategoryViewModel>()
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
