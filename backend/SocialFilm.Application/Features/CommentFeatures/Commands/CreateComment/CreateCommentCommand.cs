using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.CommentFeatures.Commands.CreateComment;

public sealed record CreateCommentCommand(string PostId, string UserId, string Message, string? PreviousCommentId) : IRequest<MessageResponse>;
