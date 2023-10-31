using SocialFilm.Domain.Entities;

using System.Linq.Expressions;

namespace SocialFilm.Application.Services;

public interface IFilmDetailService
{
    Task<bool> AnyAsync(Expression<Func<FilmDetail, bool>> filter, CancellationToken cancellationToken);
    Task SaveFilmAsync(FilmDetail filmDetail, CancellationToken cancellationToken);
    Task<FilmDetail?> GetByIdAsync(string id, CancellationToken cancellationToken);
}
