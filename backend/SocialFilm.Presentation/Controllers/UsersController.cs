using MediatR;

using Microsoft.AspNetCore.Mvc;

using SocialFilm.Application.Features.UserFeatures.Commands.AcceptFriendRequest;
using SocialFilm.Application.Features.UserFeatures.Commands.SendFriendRequest;
using SocialFilm.Application.Features.UserFeatures.Queries.GetUserById;
using SocialFilm.Application.Features.UserFeatures.Queries.GetUserFriendsById;
using SocialFilm.Application.Features.UserFeatures.Queries.GetUserProfileStatistics;
using SocialFilm.Presentation.Common;

namespace SocialFilm.Presentation.Controllers;

public sealed class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("sendFriendRequest")]
    public async Task<IActionResult> SendFriendRequestAsync([FromBody]SendFriendRequestCommand request,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("acceptFriendRequest")]
    public async Task<IActionResult> AcceptFriendRequestAsync([FromBody] AcceptFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{UserId}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("getUserFriendsById")]
    public async Task<IActionResult> GetUserFriendsByIdAsync([FromQuery] GetUserFriendsByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("getUserProfileStatistic/{UserId}")]
    public async Task<IActionResult> GetUserProfileStatisticAsync([FromRoute] GetUserProfileStatisticsCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
