namespace StayFit.Web.ViewModels.Users
{
    using System.Linq;

    using AutoMapper;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class MealDetailsUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, MealDetailsUserViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                   "/images/users/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
