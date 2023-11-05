using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;
using SocialFilm.Application.Features.PostFeatures.Commands.DeletePost;
using SocialFilm.Application.Features.PostFeatures.Queries;
using SocialFilm.Presentation.Common;

namespace SocialFilm.Presentation.Controllers;

public sealed class PostsController : BaseController
{
    public PostsController(IMediator mediator) : base(mediator)
    {
        
    }

    [HttpGet("getAllByUserId")]
    public async Task<IActionResult> GetAllByUserId([FromQuery] GetPostsOfUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePostAsync([FromForm] CreatePostCommand request,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeletePostAsync([FromBody] DeletePostCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}

