using MediatR;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.Repositories;

namespace SocialFilm.Application.Features.FilmFeatures.Commands;

public class SaveFilmCommandHandler : IRequestHandler<SaveFilmCommand, MessageResponse>
{
    private readonly IAuthService _authService;
    private readonly IFilmDetailService _filmDetailService;
    private readonly ISavedFilmService _savedFilmService;
    private readonly ITMDBApiClient _apiClient;
    public SaveFilmCommandHandler(IFilmDetailService filmDetailService, ISavedFilmService savedFilmService, ITMDBApiClient apiClient, IAuthService authService)
    {
        _filmDetailService = filmDetailService;
        _savedFilmService = savedFilmService;
        _apiClient = apiClient;
        _authService = authService;
    }
    public async Task<MessageResponse> Handle(SaveFilmCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isUserExisted = await _authService.CheckIfUserExistsAsync(request.UserId);
            if (!isUserExisted)
                throw new EntityNullException($"{request.UserId} ID içeren kullanıcı bulunamadı");

            var isFilmSavedAtDatabase = await _filmDetailService.AnyAsync(x => x.Id == request.FilmId, cancellationToken);
            if (isFilmSavedAtDatabase)
            {
                await _savedFilmService.AddOrUpdateFilmAtListOfSavedFilmOfUser(request, cancellationToken);
            }
            else
            {
                var fetchedFilm = await _apiClient.GetFilmByIdAsync(request);

                List<FilmDetailGenre> filmDetailGenres = new List<FilmDetailGenre>();

                fetchedFilm.Genre_ids.ForEach(genreId =>
                {
                    FilmDetailGenre filmDetailGenre = new FilmDetailGenre()
                    {
                        FilmDetailId = fetchedFilm.Id.ToString(),
                        GenreId = genreId.ToString()
                    };

                    filmDetailGenres.Add(filmDetailGenre);
                });

                FilmDetail filmDetail = new FilmDetail()
                {
                    Id = fetchedFilm.Id.ToString(),
                    Name = fetchedFilm.Title,
                    BackdropPath = fetchedFilm.Backdrop_path,
                    PosterPath = fetchedFilm.Poster_path,
                    Overview = fetchedFilm.Overview,
                    ReleaseYear = fetchedFilm.Release_Date,
                    FilmDetailGenres = filmDetailGenres
                };

                await _filmDetailService.SaveFilmAsync(filmDetail, cancellationToken);
            }

            return new MessageResponse("Basarili");
        }
        catch (Exception)
        {
            throw;
        }

        
    }
}
