namespace StayFit.Services.Data
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.SubCategories;

    public interface ISubCategoriesService
    {
        IEnumerable<SubCategoryViewModel> GetAllSubCategories();
    }
}
