namespace StayFit.Web.ViewModels.Categories
{
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
