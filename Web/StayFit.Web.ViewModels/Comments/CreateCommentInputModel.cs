namespace StayFit.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int MealId { get; set; }

        public string Content { get; set; }

        public int ParentId { get; set; }
    }
}
