using MediatR;

using SocialFilm.Application.Services;
using SocialFilm.Domain.DTOs;

namespace SocialFilm.Application.Features.RoleFeatures.Command;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, MessageResponse>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<MessageResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleService.CreateAsync(request);

        return new MessageResponse("Rol başarıyla oluşturuldu");
    }
}