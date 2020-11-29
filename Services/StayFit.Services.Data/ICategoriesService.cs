namespace StayFit.Services.Data
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        IEnumerable<CategoryViewModel> GetAllCategories();
    }
}
