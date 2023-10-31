using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Infrastructure.Repositories;

public sealed class SavedFilmRepository : GenericRepository<SavedFilm, AppDbContext>, ISavedFilmRepository
{
    private readonly AppDbContext _context;

    public SavedFilmRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override Task AddAsync(SavedFilm entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity.Film).State = EntityState.Unchanged;
        return base.AddAsync(entity, cancellationToken);
    }

    public override void Update(SavedFilm entity)
    {
        _context.Entry(entity.Film).State = EntityState.Unchanged;
        base.Update(entity);
    }

    public async Task<int> GetCountOfTodaySavedFilmsOfUserAsync(string userId, CancellationToken cancellationToken)
    {
        DateTime currentTime = DateTime.Now;
        DateTime thresholdTime = currentTime.Subtract(TimeSpan.FromHours(24));

        List<SavedFilm> listOfTodaySavedFilms = await GetWhere(x =>
            x.UserId == userId &&
            x.Status == SavedFilmStatus.WATCHED &&
            ((x.UpdatedAt != null && x.UpdatedAt > thresholdTime) || x.CreatedAt > thresholdTime))
        .ToListAsync();

        return listOfTodaySavedFilms.Count;
    }
}
