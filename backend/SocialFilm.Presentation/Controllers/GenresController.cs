using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.GenreFeatures.Queries.GetAllGenres;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;
using SocialFilm.Presentation.Common;

namespace SocialFilm.WebAPI.Controllers;

public sealed class GenresController : BaseController
{
    public GenresController(IMediator mediator):base(mediator)
    {
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        List<ReadGenreDTO> genres = await _mediator.Send(request, cancellationToken);
        return Ok(genres);
    }
}

