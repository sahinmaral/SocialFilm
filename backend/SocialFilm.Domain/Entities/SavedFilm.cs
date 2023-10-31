using SocialFilm.Domain.Common;
using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.Entities;

public class SavedFilm : BaseEntity
{
    public string UserId { get; set; } = null!;
    public string FilmId { get; set; } = null!;
    public FilmDetail Film { get; set; } = new FilmDetail();
    public SavedFilmStatus Status { get; set; }
}