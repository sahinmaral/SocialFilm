﻿using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public sealed class ReadPostDTO : BaseDTO
{
    public string Content { get; set; } = null!;
    public List<ReadPostPhotoDTO> Photos { get; set; } = new List<ReadPostPhotoDTO>();
}
