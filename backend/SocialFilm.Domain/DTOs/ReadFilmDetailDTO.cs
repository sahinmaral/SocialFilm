using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public class ReadFilmDetailDTO : BaseDTO
{
    public string Name { get; set; } = null!;
    public string ReleaseYear { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string PosterPath { get; set; } = null!;
    public string BackdropPath { get; set; } = null!;
}