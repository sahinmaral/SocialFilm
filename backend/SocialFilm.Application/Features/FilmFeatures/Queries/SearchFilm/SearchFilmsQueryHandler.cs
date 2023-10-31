using MediatR;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Models;

namespace SocialFilm.Application.Features.FilmFeatures.Queries;

public class SearchFilmsQueryHandler : IRequestHandler<SearchFilmsQuery, SearchFilmResponseModel>
{
    private readonly ITMDBApiClient _apiClient;

    public SearchFilmsQueryHandler(ITMDBApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<SearchFilmResponseModel> Handle(SearchFilmsQuery request, CancellationToken cancellationToken)
    {
        return await _apiClient.SearchFilmsByQueryAsync(request);
    }
}
