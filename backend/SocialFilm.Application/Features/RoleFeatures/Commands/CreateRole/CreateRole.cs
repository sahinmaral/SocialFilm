using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Features.RoleFeatures.Command.CreateRole;

public sealed record CreateRoleCommand(string Name) : IRequest<MessageResponse>;
public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, MessageResponse>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(RoleManager<Role> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<MessageResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var newRole = _mapper.Map<Role>(request);

        var result = await _roleManager.CreateAsync(newRole);
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        return new MessageResponse("Rol başarıyla oluşturuldu");
    }
}