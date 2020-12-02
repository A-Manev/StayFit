namespace StayFit.Web.ViewModels.Meals
{
    using System.Linq;

    using AutoMapper;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class MealInListViewModel : IMapFrom<Meal>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Protein { get; set; }

        public string Fat { get; set; }

        public string Carbs { get; set; }

        public string KCal { get; set; }

        public int SubCategotyId { get; set; }

        public string SubCategoryName { get; set; }

        public int CategotyId { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Meal, MealInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                   x.Images.FirstOrDefault().RemoteImageUrl != null ?
                   x.Images.FirstOrDefault().RemoteImageUrl :
                   "/images/meals/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
