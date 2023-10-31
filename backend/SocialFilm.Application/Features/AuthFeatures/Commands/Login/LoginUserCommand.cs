using MediatR;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginUserCommand(
    string UserName,
    string Password
    ) : IRequest<LoginCommandResponse>;