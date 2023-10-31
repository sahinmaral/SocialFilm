using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;

public sealed record GetSavedFilmsOfUserCommand(
    string UserId, 
    int PageNumber = 1,
    int PageSize = 10
    ) : IRequest<PaginationResult<ReadSavedFilmDto>>;
