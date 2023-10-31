using SocialFilm.Application.Features.RoleFeatures.Command;

namespace SocialFilm.Application.Services;

public interface IRoleService
{
    Task CreateAsync(CreateRoleCommand request);
}
