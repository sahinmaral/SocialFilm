using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, MessageResponse>
{
    private readonly IAuthService _authService;
    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<MessageResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await _authService.RegisterAsync(request);

        return new MessageResponse("User successfully created");
    }
}

