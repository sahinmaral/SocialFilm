using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;

using System.Linq.Expressions;

namespace SocialFilm.Persistance.Services;

public sealed class FilmDetailService : IFilmDetailService
{
    private readonly IFilmDetailRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public FilmDetailService(IFilmDetailRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AnyAsync(Expression<Func<FilmDetail, bool>> expression, CancellationToken cancellationToken)
    {
        return await _repository.AnyAsync(expression, cancellationToken);
    }

    public async Task<FilmDetail?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _repository.GetByExpressionAsync(x => x.Id == id, cancellationToken);
    }

    public async Task SaveFilmAsync(FilmDetail filmDetail, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(filmDetail, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}


