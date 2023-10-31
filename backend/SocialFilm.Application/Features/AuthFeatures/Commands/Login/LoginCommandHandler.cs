

using MediatR;

using SocialFilm.Application.Services;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginUserCommand, LoginCommandResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request);
    }
}
