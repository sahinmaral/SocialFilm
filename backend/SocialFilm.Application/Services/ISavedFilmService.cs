using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

using System.Linq.Expressions;

namespace SocialFilm.Application.Services;

public interface ISavedFilmService
{
    public Task AddOrUpdateFilmAtListOfSavedFilmOfUser(SaveFilmCommand request,CancellationToken cancellationToken);
    public IQueryable<SavedFilm> GetWhere(Expression<Func<SavedFilm,bool>> expression);
    public Task<PaginationResult<ReadSavedFilmDTO>> GetSavedFilmsByUserIdDetailedAsQueryableAsync(GetSavedFilmsOfUserCommand request);
}
