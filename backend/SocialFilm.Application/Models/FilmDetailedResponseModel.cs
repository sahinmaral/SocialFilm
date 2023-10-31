namespace SocialFilm.Application.Models;

public sealed class FilmDetailedResponseModel : FilmBaseResponseModel
{
    public List<GenreResponseModel> Genres { get; set; } = new List<GenreResponseModel>();
}

