namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.SubCategories;

    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoriesService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public IEnumerable<SubCategoryViewModel> GetAllSubCategories()
        {
            return this.subCategoryRepository
               .AllAsNoTracking()
               .To<SubCategoryViewModel>()
               .OrderBy(x => x.Name)
               .ToList();
        }
    }
}
