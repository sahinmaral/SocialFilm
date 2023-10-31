namespace SocialFilm.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommandResponse(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpires,
    string UserId
    );
