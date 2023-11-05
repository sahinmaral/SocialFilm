using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Infrastructure.Repositories;

public sealed class FilmDetailRepository : GenericRepository<FilmDetail, AppDbContext>, IFilmDetailRepository
{
    public FilmDetailRepository(AppDbContext context) : base(context)
    {
    }
}

