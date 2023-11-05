using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.UserFeatures.Queries.GetUserById;

public record GetUserByIdCommand(string UserId) : IRequest<ReadUserDTO>;