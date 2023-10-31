using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using SocialFilm.Infrastructure.Authentication;

namespace SocialFilm.Infrastructure.Authentication.OptionsSetup;

public sealed class JWTOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JWTOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection("JWT").Bind(options);
    }
}
