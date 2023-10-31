using MediatR;

using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Application.Services;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RefreshTokenAsync(request);
    }
}

