using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.UserFeatures.Commands.SendFriendRequest;

public sealed record SendFriendRequestCommand(string UserId, string FriendId) : IRequest<MessageResponse>;
