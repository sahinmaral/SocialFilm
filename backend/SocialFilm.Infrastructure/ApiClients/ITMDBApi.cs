using Refit;

using SocialFilm.Application.Features.FilmFeatures.Commands.SaveFilm;
using SocialFilm.Application.Features.FilmFeatures.Queries.SearchFilm;
using SocialFilm.Application.Models;

namespace SocialFilm.Infrastructure.ApiClients;

public interface ITMDBApi
{
    [Get("/search/movie?query={query.Name}&include_adult=false&page={query.Page}")]
    [Headers("accept: application/json", 
        "Authorization: Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlMTE2NDI2ODU1ZGFjMWFiYmQ5Yjg5MjYyZTA3Mzk0OCIsInN1YiI6IjY0NTNmYmQ5ZDhmNDRlMGRhZTk2NDRjNyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.QJJxxYcBK8_M54BdcRTfSfOvhSKvYs7nwPN1RndTQW8")]
    Task<SearchFilmResponseModel> SearchFilms(SearchFilmQuery query);

    [Get("/movie/{command.filmId}")]
    [Headers("accept: application/json",
    "Authorization: Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlMTE2NDI2ODU1ZGFjMWFiYmQ5Yjg5MjYyZTA3Mzk0OCIsInN1YiI6IjY0NTNmYmQ5ZDhmNDRlMGRhZTk2NDRjNyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.QJJxxYcBK8_M54BdcRTfSfOvhSKvYs7nwPN1RndTQW8")]
    Task<FilmResponseModel> GetFilmById(SaveFilmCommand command);
}
