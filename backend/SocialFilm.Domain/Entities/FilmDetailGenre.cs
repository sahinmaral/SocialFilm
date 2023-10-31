using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class FilmDetailGenre : BaseEntity
    {
        public string FilmDetailId { get; set; } = null!;
        public string GenreId { get; set; } = null!;
        public Genre Genre { get; set; } = new Genre();
        public FilmDetail FilmDetail { get; set; } = new FilmDetail();
    }
}