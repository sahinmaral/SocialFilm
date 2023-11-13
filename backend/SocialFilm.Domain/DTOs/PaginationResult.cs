using SocialFilm.Domain.Common;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Domain.DTOs;

public record PaginationResult<TEntity>(List<TEntity> Data, MetaData MetaData) where TEntity : BaseDTO, new();
