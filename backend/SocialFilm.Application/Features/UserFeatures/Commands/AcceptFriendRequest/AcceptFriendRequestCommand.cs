using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.UserFeatures.Commands.AcceptFriendRequest;

public sealed record AcceptFriendRequestCommand(string UserId, string FriendId) : IRequest<MessageResponse>;
