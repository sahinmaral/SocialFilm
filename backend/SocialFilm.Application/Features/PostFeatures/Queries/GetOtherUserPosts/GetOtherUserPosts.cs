using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SocialFilm.Application.Common;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Enums;
using SocialFilm.Domain.Exceptions;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.PostFeatures.Queries.GetOtherUserPosts;

public record GetOtherUserPostsCommand(
    string UserId,
    string OtherUserId,
    int PageNumber = 1,
    int PageSize = 15) : IRequest<PaginationResult<ReadPostFromOtherUserDTO>>;
public sealed class GetOtherUserPostsCommandHandler : IRequestHandler<GetOtherUserPostsCommand, PaginationResult<ReadPostFromOtherUserDTO>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetOtherUserPostsCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<PaginationResult<ReadPostFromOtherUserDTO>> Handle(GetOtherUserPostsCommand request, CancellationToken cancellationToken)
    {
        User? existedUser = await _repositoryManager
            .UserRepository
            .GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new EntityNullException($"{request.UserId} ID sahip kullanıcı bulunamadı.");

        User? existedOtherUser = await _repositoryManager
            .UserRepository
            .GetByIdAsync(request.OtherUserId, cancellationToken)
            ?? throw new EntityNullException($"{request.OtherUserId} ID sahip kullanıcı bulunamadı.");

        List<string> userSavedFilmIds = await _repositoryManager
            .SavedFilmRepository
            .GetAll()
            .Where(x => x.UserId == request.UserId && x.Status == SavedFilmStatus.WATCHED)
            .Select(x => x.FilmId).ToListAsync(cancellationToken);

        var orderedIncludedPosts = _repositoryManager.PostRepository
            .GetAll()
            .Where(x => x.UserId == request.OtherUserId)
            .Include(x => x.PostPhotos)
            .Select(x => new ReadPostFromOtherUserDTO()
            {
                Photos = _mapper.Map<List<ReadPostPhotoDTO>>(x.PostPhotos),
                Content = x.Content,
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                IsCensored = !userSavedFilmIds.Contains(x.FilmId)
            })
            .OrderByDescending(x => x.CreatedAt);

        var pagedPosts = PagedList<ReadPostFromOtherUserDTO>.ToPagedList(orderedIncludedPosts, request.PageNumber, request.PageSize);

        var mappedPosts = _mapper.Map<List<ReadPostFromOtherUserDTO>>(pagedPosts);

        return new PaginationResult<ReadPostFromOtherUserDTO>(Data: mappedPosts, MetaData: pagedPosts.MetaData);
    }
}
