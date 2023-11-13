using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.FilmFeatures.Commands.SaveFilm;
using SocialFilm.Application.Features.FilmFeatures.Queries.GetSavedFilmsOfUser;
using SocialFilm.Application.Features.FilmFeatures.Queries.SearchFilm;
using SocialFilm.Presentation.Common;

namespace SocialFilm.Presentation.Controllers;

public sealed class FilmsController : BaseController
{
    public FilmsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("searchFilms")]
    public async Task<IActionResult> SearchFilmsAsync([FromQuery]SearchFilmQuery request,CancellationToken cancellationToken)
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
    public async Task<IActionResult> GetSavedFilmsOfUserAsync([FromQuery] GetSavedFilmsOfUser request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request,cancellationToken);
        return Ok(result);
    }

    
}

