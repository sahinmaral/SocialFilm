using SocialFilm.Domain.Common;
using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.DTOs;

public class ReadSavedFilmDto : BaseDTO
{
    public ReadFilmDetailDTO Film { get; set; } = new ReadFilmDetailDTO();
    public SavedFilmStatus Status { get; set; }
}
