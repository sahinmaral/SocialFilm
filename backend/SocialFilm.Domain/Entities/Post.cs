using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string FilmId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<PostPhoto> PostPhotos { get; set; } = new List<PostPhoto>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}