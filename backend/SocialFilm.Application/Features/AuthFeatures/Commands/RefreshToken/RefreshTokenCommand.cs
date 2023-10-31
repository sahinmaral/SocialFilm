using MediatR;

using SocialFilm.Application.Features.AuthFeatures.Commands.Login;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string UserId,
    string RefreshToken) : IRequest<LoginCommandResponse>;

