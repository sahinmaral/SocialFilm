using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.GenreFeatures.Queries.GetAllGenre;
using SocialFilm.Domain.Entities;
using SocialFilm.Presentation.Common;

namespace SocialFilm.WebAPI.Controllers;

public sealed class GenresController : BaseController
{
    public GenresController(IMediator mediator):base(mediator)
    {
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllGenreQuery request, CancellationToken cancellationToken)
    {
        List<Genre> genres = await _mediator.Send(request, cancellationToken);
        return Ok(genres);
    }
}

