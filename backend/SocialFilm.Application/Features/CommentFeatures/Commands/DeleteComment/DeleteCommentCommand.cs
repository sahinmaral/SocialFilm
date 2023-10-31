using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.CommentFeatures.Commands.DeleteComment;

public sealed record DeleteCommentCommand(string CommentId) : IRequest<MessageResponse>;

