using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using SocialFilm.Application.Abstractions;
using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Application.Features.AuthFeatures.Commands.RefreshToken;


public sealed record RefreshTokenCommand(
    string UserId,
    string RefreshToken) : IRequest<LoginCommandResponse>;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public RefreshTokenCommandHandler(UserManager<User> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<LoginCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userManager.FindByIdAsync(request.UserId) 
            ?? throw new EntityNullException("Bu kullanıcı id ye sahip bir kullanıcı yok");

        if (user.RefreshToken != request.RefreshToken)
            throw new SecurityTokenException("Refresh token geçerli değil");

        if (user.RefreshTokenExpires < DateTime.Now)
            throw new SecurityTokenException("Refresh token süresi dolmuş");

        LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);

        return response;
    }
}

