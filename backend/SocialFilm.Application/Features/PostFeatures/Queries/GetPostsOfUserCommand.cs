using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.PostFeatures.Queries;

public record GetPostsOfUserCommand(
    string UserId,
    int PageNumber = 1,
    int PageSize = 15): IRequest<PaginationResult<ReadPostDTO>>;
