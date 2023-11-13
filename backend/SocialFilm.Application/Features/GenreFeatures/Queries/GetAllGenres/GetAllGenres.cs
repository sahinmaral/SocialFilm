using AutoMapper;

using MediatR;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.GenreFeatures.Queries.GetAllGenres;

public sealed record GetAllGenresQuery() : IRequest<List<ReadGenreDTO>>;
public sealed class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, List<ReadGenreDTO>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetAllGenresQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public Task<List<ReadGenreDTO>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = _repositoryManager
            .GenreRepository
            .GetAll()
            .OrderBy(x => x.Name);

        var mappedGenres = _mapper.Map<List<ReadGenreDTO>>(genres);

        return Task.FromResult(mappedGenres);
    }
}
