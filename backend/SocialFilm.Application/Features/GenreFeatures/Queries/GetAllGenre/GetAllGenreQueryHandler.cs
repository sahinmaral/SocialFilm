using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Features.GenreFeatures.Queries.GetAllGenre;

public sealed class GetAllGenreQueryHandler : IRequestHandler<GetAllGenreQuery, List<Genre>>
{
    private readonly IGenreService _genreService;

    public GetAllGenreQueryHandler(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public async Task<List<Genre>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
    {
        return await _genreService.GetAllAsync(cancellationToken);
    }
}
