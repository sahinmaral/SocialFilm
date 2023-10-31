using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Models;

namespace SocialFilm.Application.ApiClients;

public interface ITMDBApiClient
{
    Task<SearchFilmResponseModel> SearchFilmsByQueryAsync(SearchFilmsQuery query);
    Task<FilmResponseModel> GetFilmByIdAsync(SaveFilmCommand command);
}
