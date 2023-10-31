using SocialFilm.Domain.Common;

namespace SocialFilm.Domain.DTOs;

public record PaginationResult<TEntity>(
    List<TEntity> Datas,
    int PageNumber,
    int PageSize,
    int TotalPages,
    int TotalRecords,
    bool IsFirstPage,
    bool IsLastPage) where TEntity : new();
