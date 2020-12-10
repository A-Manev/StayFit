namespace StayFit.Web.ViewModels.Comments
{
    using System;

    using AutoMapper;
    using Ganss.XSS;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class MealCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public int CommentLikes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, MealCommentViewModel>()
                 .ForMember(x => x.CommentLikes, options =>
                 {
                     options.MapFrom(x => x.Likes.Count);
                 });
        }
    }
}
