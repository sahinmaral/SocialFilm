using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;

public sealed record GetSavedFilmsOfUser(
    string UserId,
    int PageNumber = 1,
    int PageSize = 10
    ) : IRequest<PaginationResult<ReadSavedFilmDTO>>;

public sealed class GetSavedFilmsOfUserCommandHandler : IRequestHandler<GetSavedFilmsOfUser, PaginationResult<ReadSavedFilmDTO>>
{
    private readonly ISavedFilmRepository _savedFilmRepository;
    private readonly IMapper _mapper;
    public GetSavedFilmsOfUserCommandHandler(IMapper mapper, ISavedFilmRepository savedFilmRepository)
    {
        _mapper = mapper;
        _savedFilmRepository = savedFilmRepository;
    }

    public Task<PaginationResult<ReadSavedFilmDTO>> Handle(GetSavedFilmsOfUser request, CancellationToken cancellationToken)
    {
        var includedSavedFilms = _savedFilmRepository
                        .GetWhere(x => x.UserId == request.UserId)
                            .Include(x => x.Film)
                                .ThenInclude(x => x.FilmDetailGenres)
                                    .ThenInclude(x => x.Genre);

        var sortedIncludedSavedFilms = includedSavedFilms
            .OrderBy(x => x.CreatedAt);

        var pagedSortedIncludedSavedFilms = PagedList<SavedFilm>
            .ToPagedList(sortedIncludedSavedFilms, request.PageNumber, request.PageSize);

        var mappedSavedFilms = _mapper.Map<IEnumerable<ReadSavedFilmDTO>>(pagedSortedIncludedSavedFilms);
        var listedSavedFilms = mappedSavedFilms.ToList();

        var result = new PaginationResult<ReadSavedFilmDTO>(Data: listedSavedFilms, MetaData: pagedSortedIncludedSavedFilms.MetaData);

        return Task.FromResult(result);
    }
}
