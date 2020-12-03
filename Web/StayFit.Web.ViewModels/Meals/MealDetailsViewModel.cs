namespace StayFit.Web.ViewModels.Meals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;

    public class MealDetailsViewModel : IMapFrom<Meal>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string PreparationTime { get; set; }

        public string CookingTime { get; set; }

        public SkillLevel SkillLevel { get; set; }

        public string PortionCount { get; set; }

        public double KCal { get; set; }

        public double Fat { get; set; }

        public double Saturates { get; set; }

        public double Carbs { get; set; }

        public double Sugars { get; set; }

        public double Fibre { get; set; }

        public double Protein { get; set; }

        public double Salt { get; set; }

        public string Description { get; set; }

        public string MethodOfPreparation { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AddedByUserUserName { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public IEnumerable<IngredientsViewModel> Ingredients { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Meal, MealDetailsViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                   x.Images.FirstOrDefault().RemoteImageUrl != null ?
                   x.Images.FirstOrDefault().RemoteImageUrl :
                   "/images/meals/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
