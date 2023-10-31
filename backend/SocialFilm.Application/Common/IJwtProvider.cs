

using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Domain.Entities;

namespace SocialFilm.Application.Abstractions;

public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateTokenAsync(User user);
}
