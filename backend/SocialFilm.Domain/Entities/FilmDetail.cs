using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.Entities
{
    public class FilmDetail : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string ReleaseYear { get; set; } = null!;
        public string Overview { get; set; } = null!;
        public string PosterPath { get; set; } = null!;
        public string BackdropPath { get; set; } = null!;
        public List<FilmDetailGenre> FilmDetailGenres { get; set; } = new List<FilmDetailGenre>();
        public List<SavedFilm> SavedFilms { get; set; } = new List<SavedFilm>();
    }
}