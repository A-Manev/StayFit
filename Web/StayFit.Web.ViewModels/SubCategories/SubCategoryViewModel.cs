namespace StayFit.Web.ViewModels.SubCategories
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class SubCategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
