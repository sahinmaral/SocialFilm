using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? PreviousCommentId { get; set; }
        public Comment? PreviousComment { get; set; }
        public List<Comment> SubComments { get; set; } = new List<Comment>();
    }
}