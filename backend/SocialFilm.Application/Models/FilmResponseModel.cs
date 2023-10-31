namespace SocialFilm.Application.Models;

public sealed class FilmResponseModel : FilmBaseResponseModel
{
    public List<int> Genre_ids { get; set; } = new List<int>();
}
