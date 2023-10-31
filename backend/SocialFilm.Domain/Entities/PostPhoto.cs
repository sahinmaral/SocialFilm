using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class PostPhoto : BaseEntity
    {
        public Post Post { get; set; } = new Post();
        public string PostId { get; set; } = null!;
        public string PhotoPath { get; set; } = null!;
    }
}