using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public sealed class ReadPostDetailedDTO : BaseDTO
{
    public string Content { get; set; } = null!;
    public ReadUserDTO User { get; set; } = null!;
    public List<ReadPostPhotoDTO> Photos { get; set; } = null!;
    public ReadFilmDetailDTO FilmDetail { get; set; } = null!;
    public int CommentCount { get; set; }
}

