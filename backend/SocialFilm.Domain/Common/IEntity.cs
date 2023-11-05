﻿namespace SocialFilm.Domain.Common;

public interface IEntity
{
    string Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}