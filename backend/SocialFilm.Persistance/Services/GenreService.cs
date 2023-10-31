using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Persistance.Services;

public sealed class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Genre>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAll().ToListAsync(cancellationToken);
    }

}
