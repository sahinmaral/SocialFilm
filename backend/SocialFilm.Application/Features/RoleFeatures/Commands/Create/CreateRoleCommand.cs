using MediatR;

using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.RoleFeatures.Command;

public sealed record CreateRoleCommand(string Name): IRequest<MessageResponse>;
