using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Persistance.Extensions;

public static class EntityPaginationExtension
{
    public static async Task<PaginationResult<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> data, int pageSize, int pageNumber)
        where TEntity : new()
    {
        int totalDataCount = await data.CountAsync();

        int totalPages = (int)Math.Ceiling(totalDataCount / (double)pageSize);
        bool isFirstPage = pageNumber == 1;
        bool isLastPage = pageNumber == totalPages;

        var pagedData = await data
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<TEntity>(pagedData, pageNumber, pageSize, totalPages, totalDataCount, isFirstPage, isLastPage);
    }
}
