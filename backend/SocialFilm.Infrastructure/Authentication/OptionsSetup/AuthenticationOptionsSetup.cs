using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace SocialFilm.Infrastructure.Authentication.OptionsSetup;

public sealed class AuthenticationOptionsSetup : IPostConfigureOptions<AuthenticationOptions>
{
    public void PostConfigure(string? name, AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}
