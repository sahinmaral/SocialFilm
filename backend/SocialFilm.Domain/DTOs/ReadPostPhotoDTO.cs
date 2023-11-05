using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public class ReadPostPhotoDTO : BaseDTO
{
    public string PhotoPath { get; set; } = null!;
}