using SocialFilm.Application.Features.FilmFeatures.Commands.SaveFilm;
using SocialFilm.Application.Features.FilmFeatures.Queries.SearchFilm;
using SocialFilm.Application.Models;

namespace SocialFilm.Application.ApiClients;

public interface ITMDBApiClient
{
    Task<SearchFilmResponseModel> SearchFilmsByQueryAsync(SearchFilmQuery query);
    Task<FilmResponseModel> GetFilmByIdAsync(SaveFilmCommand command);
}
