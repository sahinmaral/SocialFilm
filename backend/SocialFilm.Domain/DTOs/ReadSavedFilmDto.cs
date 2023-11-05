using SocialFilm.Domain.Common;
using SocialFilm.Domain.Enums;

namespace SocialFilm.Domain.DTOs;

public class ReadSavedFilmDTO : BaseDTO
{
    public SavedFilmStatus Status { get; set; }
    public ReadFilmDetailDTO Film { get; set; } = new ReadFilmDetailDTO();
}

