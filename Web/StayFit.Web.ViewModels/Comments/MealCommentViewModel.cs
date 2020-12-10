namespace StayFit.Web.ViewModels.Comments
{
    using System;

    using Ganss.XSS;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class MealCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
    }
}
