using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.PostFeatures.Queries;

public sealed class GetPostsOfUserCommandHandler : IRequestHandler<GetPostsOfUserCommand, PaginationResult<ReadPostDTO>>
{
    private readonly IPostService _postService;

    public GetPostsOfUserCommandHandler(IPostService postService)
    {
        _postService = postService;
    }

    public async Task<PaginationResult<ReadPostDTO>> Handle(GetPostsOfUserCommand request, CancellationToken cancellationToken)
    {
        return await _postService.GetAllByUserIdAsPaginatedAsync(request.UserId,request.PageSize,request.PageNumber);
    }
}
