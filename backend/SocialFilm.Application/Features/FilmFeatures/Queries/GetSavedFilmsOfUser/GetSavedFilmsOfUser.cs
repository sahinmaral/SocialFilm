using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Domain.RequestFeatures;
using SocialFilm.Domain.Extensions;

namespace SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;

public sealed record GetSavedFilmsOfUserCommand(
    string UserId,
    SavedFilmParameters? Parameters = null
    ) : IRequest<PaginationResult<ReadSavedFilmDTO>>;

public sealed class GetSavedFilmsOfUserCommandHandler : IRequestHandler<GetSavedFilmsOfUserCommand, PaginationResult<ReadSavedFilmDTO>>
{
    private readonly ISavedFilmRepository _savedFilmRepository;
    private readonly IMapper _mapper;
    public GetSavedFilmsOfUserCommandHandler(IMapper mapper, ISavedFilmRepository savedFilmRepository)
    {
        _mapper = mapper;
        _savedFilmRepository = savedFilmRepository;
    }

    public Task<PaginationResult<ReadSavedFilmDTO>> Handle(GetSavedFilmsOfUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Parameters == null)
        {
            request = new GetSavedFilmsOfUserCommand(UserId: request.UserId)
            {
                Parameters = new SavedFilmParameters() 
            };
        }

        var includedSavedFilms = _savedFilmRepository
                        .GetWhere(x => x.UserId == request.UserId)
                            .Include(x => x.Film)
                                 .ThenInclude(x => x.FilmDetailGenres)
                                    .ThenInclude(x => x.Genre)
                                        .AsQueryable()
                                        .FilterByStatus(request.Parameters.Status)
                                        .SearchByFilmName(request.Parameters.SearchTerm);


        var sortedIncludedSavedFilms = includedSavedFilms
            .OrderByDescending(x => x.CreatedAt);

        var pagedSortedIncludedSavedFilms = PagedList<SavedFilm>
            .ToPagedList(sortedIncludedSavedFilms, 
                            request.Parameters.PaginationParameters.CurrentPage, 
                            request.Parameters.PaginationParameters.PageSize
            );

        var mappedSavedFilms = _mapper.Map<List<ReadSavedFilmDTO>>(pagedSortedIncludedSavedFilms);

        var result = new PaginationResult<ReadSavedFilmDTO>(Data: mappedSavedFilms, MetaData: pagedSortedIncludedSavedFilms.MetaData);

        return Task.FromResult(result);
    }
}
