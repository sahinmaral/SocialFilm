using MediatR;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.Models;

namespace SocialFilm.Application.Features.FilmFeatures.Queries.SearchFilm;

public sealed record SearchFilmQuery(string Name, string? ReleaseYear, int Page = 1) : IRequest<SearchFilmResponseModel>;

public sealed class SearchFilmQueryHandler : IRequestHandler<SearchFilmQuery, SearchFilmResponseModel>
{
    private readonly ITMDBApiClient _apiClient;

    public SearchFilmQueryHandler(ITMDBApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<SearchFilmResponseModel> Handle(SearchFilmQuery request, CancellationToken cancellationToken)
    {
        return await _apiClient.SearchFilmsByQueryAsync(request);
    }
}




