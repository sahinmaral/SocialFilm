using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Application.Features.AuthFeatures.Commands.RefreshToken;
using SocialFilm.Application.Features.AuthFeatures.Commands.Register;

namespace SocialFilm.Application.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterUserCommand request);
    Task<LoginCommandResponse> LoginAsync(LoginUserCommand request);
    Task<LoginCommandResponse> RefreshTokenAsync(RefreshTokenCommand request);
    Task<bool> CheckIfUserExistsAsync(string id);
}
