using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.FilmFeatures.Commands;
using SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;
using SocialFilm.Presentation.Common;

namespace SocialFilm.Presentation.Controllers;

public sealed class FilmsController : BaseController
{
    public FilmsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("searchFilms")]
    public async Task<IActionResult> SearchFilmsAsync([FromQuery]SearchFilmsQuery request,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("saveFilm")]
    public async Task<IActionResult> SaveFilmAsync([FromBody]SaveFilmCommand request,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("getSavedFilmsOfUser")]
    public async Task<IActionResult> GetSavedFilmsOfUser([FromQuery] GetSavedFilmsOfUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    
}

