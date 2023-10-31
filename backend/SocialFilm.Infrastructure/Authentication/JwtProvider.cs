using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SocialFilm.Application.Abstractions;
using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Domain.Entities;
using SocialFilm.Infrastructure.Authentication;
using SocialFilm.Infrastructure.Authentication.Extensions;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace SocialFilm.Infrastructure.Identity
{

    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<User> _userManager;

        public JwtProvider(IOptions<JwtOptions> jwtOptions, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<LoginCommandResponse> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Name,user.UserName)
            };

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            claims.AddRoles(roles.ToArray());

            var expires = DateTime.Now.ToLocalTime().AddHours(1);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature));

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = expires.AddMinutes(15);

            await _userManager.UpdateAsync(user);

            LoginCommandResponse response = new LoginCommandResponse(
                AccessToken: token,
                RefreshToken: refreshToken,
                RefreshTokenExpires: Convert.ToDateTime(user.RefreshTokenExpires),
                UserId: user.Id
                );

            return response;
        }

        private string GenerateRefreshToken()
        {
            Guid refreshTokenGuid = Guid.NewGuid();
            return refreshTokenGuid.ToString();
        }
    }
}
