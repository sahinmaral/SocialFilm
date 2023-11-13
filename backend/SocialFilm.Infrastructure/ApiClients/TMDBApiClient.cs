using Refit;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.Features.FilmFeatures.Commands.SaveFilm;
using SocialFilm.Application.Features.FilmFeatures.Queries.SearchFilm;
using SocialFilm.Application.Models;

namespace SocialFilm.Infrastructure.ApiClients;

public class TMDBApiClient : ITMDBApiClient
{
    public async Task<SearchFilmResponseModel> SearchFilmsByQueryAsync(SearchFilmQuery query)
    {
        //TODO: Buradan gelecek olan butun hatalarin handle edilmesi gerekiyor
        //TODO: Rate limite bazen yakalanabiliriz. Dokumantasyonu uzerinde 429 hatasi ile gelir.

        ITMDBApi filmAPI = RestService.For<ITMDBApi>("https://api.themoviedb.org/3");
        return await filmAPI.SearchFilms(query);
    }

    public async Task<FilmResponseModel> GetFilmByIdAsync(SaveFilmCommand command)
    {
        ITMDBApi filmAPI = RestService.For<ITMDBApi>("https://api.themoviedb.org/3");
        return await filmAPI.GetFilmById(command);
    }
}
