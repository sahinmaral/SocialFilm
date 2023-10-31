using SocialFilm.Application.Features.GenreFeatures.Queries.GetAllGenre;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Services;

public interface IGenreService
{
    Task<List<Genre>> GetAllAsync(CancellationToken cancellationToken);
}
