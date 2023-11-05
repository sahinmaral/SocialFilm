using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

using System.Linq.Expressions;

namespace SocialFilm.Application.Services;

public interface ISavedFilmService
{
    Task AddOrUpdateFilmAtListOfSavedFilmOfUser(SaveFilmCommand request,CancellationToken cancellationToken);
    IQueryable<SavedFilm> GetWhere(Expression<Func<SavedFilm,bool>> expression);
    Task<PaginationResult<ReadSavedFilmDTO>> GetSavedFilmsByUserIdDetailedAsPaginatedAsync(string userId, int pageSize, int pageNumber);
}
