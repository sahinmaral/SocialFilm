using AutoMapper;

using Microsoft.AspNetCore.Identity;

using SocialFilm.Application.Features.RoleFeatures.Command;
using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Persistance.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<Role> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateRoleCommand request)
    {
        var newRole = _mapper.Map<Role>(request);

        var result = await _roleManager.CreateAsync(newRole);
        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);
    }
}
