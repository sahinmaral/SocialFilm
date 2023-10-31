namespace SocialFilm.Application.Models
{
    public sealed class SearchFilmResponseModel
    {
        public int Page { get; set; }
        public List<FilmResponseModel> Results { get; set; } = new List<FilmResponseModel>();
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}


