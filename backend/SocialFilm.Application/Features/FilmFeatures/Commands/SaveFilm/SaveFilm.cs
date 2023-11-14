using AutoMapper;

using MediatR;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Exceptions;

using static System.Net.Mime.MediaTypeNames;

namespace SocialFilm.Application.Features.FilmFeatures.Commands.SaveFilm;

public sealed record SaveFilmCommand(string FilmId, string UserId, SavedFilmStatus Status) : IRequest<MessageResponse>;
public sealed class SaveFilmCommandHandler : IRequestHandler<SaveFilmCommand, MessageResponse>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ITMDBApiClient _apiClient;
    private readonly IMapper _mapper;
    public SaveFilmCommandHandler(ITMDBApiClient apiClient, IRepositoryManager repositoryManager, IMapper mapper)
    {
        _apiClient = apiClient;
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    private async Task<bool> CheckIfUserSavedMaximumThreeFilmsToday(string userId, CancellationToken cancellationToken)
    {
        int count = await _repositoryManager.SavedFilmRepository.GetCountOfTodaySavedFilmsOfUserAsync(userId, cancellationToken);
        return count == 3;
    }

    public async Task<MessageResponse> Handle(SaveFilmCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existedUser = await _repositoryManager
                .UserRepository
                .GetByIdAsync(request.UserId,cancellationToken) 
                ?? throw new EntityNullException($"{request.UserId} ID içeren kullanıcı bulunamadı");

            if (await CheckIfUserSavedMaximumThreeFilmsToday(request.UserId, cancellationToken))
                throw new InvalidOperationException("Mental sagliginiz acisindan gunde uc tane film izleyebilirsiniz. Bunun yerine dinlenebilirsiniz veya seçtiğiniz filmi izlenmemiş olarak kaydedebilirsiniz.");

            var isFilmSavedAtDatabase = await _repositoryManager
                .FilmDetailRepository
                .AnyAsync(x => x.Id == request.FilmId, cancellationToken);

            if (isFilmSavedAtDatabase)
            {

                var filmDetail = await _repositoryManager
                    .FilmDetailRepository
                    .GetByIdAsync(request.FilmId, cancellationToken) 
                    ?? throw new EntityNullException($"{request.FilmId} ID sahip film bulunamadı.");
                var savedFilm =
                        await _repositoryManager.SavedFilmRepository
                        .GetByExpressionAsync(
                            x => x.UserId == request.UserId &&
                            x.FilmId == request.FilmId,
                            cancellationToken
                        );

                if (savedFilm is null)
                {
                    var newSavedFilm = _mapper.Map<SavedFilm>(request);
                    newSavedFilm.Film = filmDetail;

                    await _repositoryManager.SavedFilmRepository.AddAsync(newSavedFilm, cancellationToken);
                }
                else
                {
                    if (savedFilm.Status == request.Status)
                        throw new InvalidOperationException("Bu filmi zaten kaydettiniz.");

                    savedFilm.Film = filmDetail;
                    savedFilm.Status = request.Status;
                    _repositoryManager.SavedFilmRepository.Update(savedFilm);
                }
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

                SavedFilm newEntity = new SavedFilm()
                {
                    FilmId = filmDetail.Id,
                    Status = request.Status,
                    UserId = request.UserId
                };

                filmDetail.SavedFilms.Add(newEntity);

                await _repositoryManager.FilmDetailRepository.AddAsync(filmDetail, cancellationToken);

                await _repositoryManager.SaveChangesAsync(cancellationToken);

            }

            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return new MessageResponse("Basarili");
        }
        catch (Exception)
        {
            throw;
        }


    }
}
