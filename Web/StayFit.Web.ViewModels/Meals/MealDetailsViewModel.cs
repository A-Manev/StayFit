namespace StayFit.Web.ViewModels.Meals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Comments;

    public class MealDetailsViewModel : IMapFrom<Meal>, IHaveCustomMappings
    {
        public int Id { get; set; }

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

        public double AverageVote { get; set; }

        public IEnumerable<MealCommentViewModel> Comments { get; set; }

        public string SanitizedMethodOfPreparation => new HtmlSanitizer().Sanitize(this.MethodOfPreparation);

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public IEnumerable<IngredientsViewModel> Ingredients { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Meal, MealDetailsViewModel>()
                 .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
                .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x =>
                   x.Images.FirstOrDefault().RemoteImageUrl != null ?
                   x.Images.FirstOrDefault().RemoteImageUrl :
                   "/images/meals/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
