using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;
using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.Repositories;
using SocialFilm.Persistance.Extensions;

using System.Linq.Expressions;

namespace SocialFilm.Persistance.Services;

public sealed class SavedFilmService : ISavedFilmService
{
    private readonly ISavedFilmRepository _savedFilmRepository;
    private readonly IFilmDetailService _filmDetailService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SavedFilmService(ISavedFilmRepository repository, IMapper mapper, IUnitOfWork unitOfWork, IFilmDetailService filmDetailService)
    {
        _savedFilmRepository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _filmDetailService = filmDetailService;
    }

    private async Task<bool> CheckIfUserSavedMaximumThreeFilmsToday(string userId, CancellationToken cancellationToken)
    {
        int count = await _savedFilmRepository.GetCountOfTodaySavedFilmsOfUserAsync(userId, cancellationToken);
        return count == 3;
    }

    public async Task AddOrUpdateFilmAtListOfSavedFilmOfUser(SaveFilmCommand request, CancellationToken cancellationToken)
    {
        if (await CheckIfUserSavedMaximumThreeFilmsToday(request.UserId, cancellationToken))
            throw new InvalidOperationException("Mental sagliginiz acisindan gunde uc tane film izleyebilirsiniz. Bunun yerine dinlenebilirsiniz veya seçtiğiniz filmi izlenmemiş olarak kaydedebilirsiniz.");

        var filmDetail = await _filmDetailService.GetByIdAsync(request.FilmId, cancellationToken);
        if (filmDetail == null)
            throw new EntityNullException($"{request.FilmId} ID sahip film bulunamadı.");

        var savedFilm =
                await _savedFilmRepository
                .GetByExpressionAsync(
                    x => x.UserId == request.UserId &&
                    x.FilmId == request.FilmId,
                    cancellationToken
                );

        if (savedFilm == null)
        {
            var newSavedFilm = _mapper.Map<SavedFilm>(request);
            newSavedFilm.Film = filmDetail;

            await _savedFilmRepository.AddAsync(newSavedFilm, cancellationToken);
        }
        else
        {
            if (savedFilm.Status == request.Status)
                throw new InvalidOperationException("Bu filmi zaten kaydettiniz.");

            savedFilm.Film = filmDetail;
            savedFilm.Status = request.Status;
            _savedFilmRepository.Update(savedFilm);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<SavedFilm> GetWhere(Expression<Func<SavedFilm, bool>> expression)
    {
        return _savedFilmRepository.GetWhere(expression);
    }

    public async Task<PaginationResult<ReadSavedFilmDTO>> GetSavedFilmsByUserIdDetailedAsQueryableAsync(GetSavedFilmsOfUserCommand request)
    {
        return await _savedFilmRepository
            .GetWhere(x => x.UserId == request.UserId)
            .Include(x => x.Film)
                .ThenInclude(x => x.FilmDetailGenres)
                    .ThenInclude(x => x.Genre)
            .Select(x => _mapper.Map<ReadSavedFilmDTO>(x))
            .ToPagedListAsync(request.PageSize,request.PageNumber);
    }
}
