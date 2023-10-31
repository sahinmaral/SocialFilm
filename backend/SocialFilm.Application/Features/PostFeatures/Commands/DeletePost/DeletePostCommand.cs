using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.PostFeatures.Commands.DeletePost;

public sealed record DeletePostCommand(string PostId) : IRequest<MessageResponse>;
