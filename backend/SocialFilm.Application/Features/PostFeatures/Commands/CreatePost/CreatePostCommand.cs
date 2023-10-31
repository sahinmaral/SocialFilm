using MediatR;

using Microsoft.AspNetCore.Http;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.PostFeatures.Commands.CreatePost;

public sealed record CreatePostCommand(string FilmId, string UserId, string Content, List<IFormFile> Files) : IRequest<MessageResponse>;
