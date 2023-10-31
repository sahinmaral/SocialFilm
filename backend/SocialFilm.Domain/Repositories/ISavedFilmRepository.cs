using SocialFilm.Domain.Entities;

namespace SocialFilm.Domain.Repositories;

public interface ISavedFilmRepository : IRepository<SavedFilm>
{
    public Task<int> GetCountOfTodaySavedFilmsOfUserAsync(string userId, CancellationToken cancellationToken);
    new Task AddAsync(SavedFilm savedFilm, CancellationToken cancellationToken = default);
}
