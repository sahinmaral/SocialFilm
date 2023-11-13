using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Application.Features.AuthFeatures.Commands.RefreshToken;
using SocialFilm.Application.Features.AuthFeatures.Commands.Register;
using SocialFilm.Presentation.Common;

namespace SocialFilm.Presentation.Controllers;

public sealed class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand request,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        LoginCommandResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        LoginCommandResponse response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

}
