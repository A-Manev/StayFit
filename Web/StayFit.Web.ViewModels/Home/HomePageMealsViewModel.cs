namespace StayFit.Web.ViewModels.Home
{
    using System.Linq;

    using AutoMapper;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class HomePageMealsViewModel : IMapFrom<Meal>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Meal, HomePageMealsViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                   x.Images.FirstOrDefault().RemoteImageUrl != null ?
                   x.Images.FirstOrDefault().RemoteImageUrl :
                   "/images/meals/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
