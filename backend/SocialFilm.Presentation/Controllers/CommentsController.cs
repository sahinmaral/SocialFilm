using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;
using SocialFilm.Application.Features.CommentFeatures.Commands.DeleteComment;
using SocialFilm.Presentation.Common;

namespace SocialFilm.Presentation.Controllers;

public sealed class CommentsController : BaseController
{
    public CommentsController(IMediator mediator) : base(mediator)
    {

    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteCommentAsync([FromBody] DeleteCommentCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

}

