

using MediatR;

using Microsoft.AspNetCore.Identity;

using SocialFilm.Application.Abstractions;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.Login;


public sealed record LoginCommand(
    string UserName,
    string Password
    ) : IRequest<LoginCommandResponse>;
public sealed record LoginCommandResponse(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpires,
    string UserId
    );

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(UserManager<User> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userManager.FindByNameAsync(request.UserName) 
            ?? throw new EntityNullException("Bu kullanıcı adında ya da mail adresine sahip bir kullanıcı yok");

        var passwordCheckResult = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordCheckResult)
            throw new InvalidOperationException("Girdiğiniz kullanıcı adı veya email ile birlikte şifreyi yanlış girdiniz");

        if (!user.EmailConfirmed)
            throw new InvalidOperationException("Lütfen kayıt olduğunuz email adresi içerisinde gelen mail üzerinden hesabınızı onaylayınız");

        LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);

        return response;
    }
}
