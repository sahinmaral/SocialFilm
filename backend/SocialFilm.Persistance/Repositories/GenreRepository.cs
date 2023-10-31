using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Context;

namespace SocialFilm.Infrastructure.Repositories;

public sealed class GenreRepository : GenericRepository<Genre, AppDbContext>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context)
    {
    }
}
