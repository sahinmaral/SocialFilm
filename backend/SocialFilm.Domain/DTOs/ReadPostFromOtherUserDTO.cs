using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public sealed class ReadPostFromOtherUserDTO : ReadPostDTO
{
    public bool IsCensored { get; set; }
}

