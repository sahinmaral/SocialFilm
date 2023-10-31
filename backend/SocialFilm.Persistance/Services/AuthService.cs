using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using SocialFilm.Application.Abstractions;
using SocialFilm.Application.Features.AuthFeatures.Commands.Login;
using SocialFilm.Application.Features.AuthFeatures.Commands.RefreshToken;
using SocialFilm.Application.Features.AuthFeatures.Commands.Register;
using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Exceptions;

namespace SocialFilm.Persistance.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, IJwtProvider jwtProvider, IMapper mapper)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<LoginCommandResponse> LoginAsync(LoginUserCommand request)
        {
            User? user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new EntityNullException("Bu kullanıcı adında ya da mail adresine sahip bir kullanıcı yok");

            var passwordCheckResult = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordCheckResult)
                throw new InvalidOperationException("Girdiğiniz kullanıcı adı veya email ile birlikte şifreyi yanlış girdiniz");

            if (!user.EmailConfirmed)
                throw new InvalidOperationException("Lütfen kayıt olduğunuz email adresi içerisinde gelen mail üzerinden hesabınızı onaylayınız");

            LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);

            return response;
        }

        public async Task RegisterAsync(RegisterUserCommand request)
        {
            var newUser = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));
                throw new Exception(errorMessage);
            }

            await _userManager.AddToRoleAsync(newUser, "User");
        }

        public async Task<LoginCommandResponse> RefreshTokenAsync(RefreshTokenCommand request)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                throw new EntityNullException("Bu kullanıcı id ye sahip bir kullanıcı yok");

            if (user.RefreshToken != request.RefreshToken)
                throw new SecurityTokenException("Refresh token geçerli değil");

            if (user.RefreshTokenExpires < DateTime.Now)
                throw new SecurityTokenException("Refresh token süresi dolmuş");

            LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);
            return response;
        }

        public async Task<bool> CheckIfUserExistsAsync(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);

            return user != null;
        }
    }
}
