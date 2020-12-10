namespace StayFit.Data.Models
{
    using StayFit.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsLiked { get; set; }
    }
}
