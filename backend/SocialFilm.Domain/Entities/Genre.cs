using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<FilmDetailGenre> FilmDetailGenres { get; set; } = new List<FilmDetailGenre>();
    }
}