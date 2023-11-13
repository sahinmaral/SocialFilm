using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.PostFeatures.Queries.GetAllByUserId;

public record GetAllByUserIdCommand(
    string UserId,
    int PageNumber = 1,
    int PageSize = 15) : IRequest<PaginationResult<ReadPostDTO>>;
public sealed class GetAllByUserIdCommandHandler : IRequestHandler<GetAllByUserIdCommand, PaginationResult<ReadPostDTO>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetAllByUserIdCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<PaginationResult<ReadPostDTO>> Handle(GetAllByUserIdCommand request, CancellationToken cancellationToken)
    {
        User? existedUser = await _repositoryManager
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı.");

        var orderedIncludedPosts = _repositoryManager.PostRepository
            .GetAll()
            .Where(x => x.UserId == request.UserId)
            .Include(x => x.PostPhotos)
            .OrderByDescending(x => x.CreatedAt);

        var pagedPosts = PagedList<Post>.ToPagedList(orderedIncludedPosts, request.PageNumber, request.PageSize);

        var mappedPosts = _mapper.Map<List<ReadPostDTO>>(pagedPosts);

        return new PaginationResult<ReadPostDTO>(Data: mappedPosts, MetaData: pagedPosts.MetaData);
    }
}
