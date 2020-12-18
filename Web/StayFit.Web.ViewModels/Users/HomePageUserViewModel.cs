namespace StayFit.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class HomePageUserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public double DailyCalories { get; set; }

        public double RemainingCalories { get; set; }

        public double EatenFood => this.DailyCalories - this.RemainingCalories;

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Add at least one image.")]
        public IFormFile Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, HomePageUserViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                   opt.MapFrom(x =>
                   "/images/users/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
