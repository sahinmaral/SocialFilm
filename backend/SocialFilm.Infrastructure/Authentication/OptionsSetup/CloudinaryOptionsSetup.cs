using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SocialFilm.Infrastructure.Authentication.OptionsSetup;

public sealed class CloudinaryOptionsSetup : IConfigureOptions<CloudinaryOptions>
{
    private readonly IConfiguration _configuration;

    public CloudinaryOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(CloudinaryOptions options)
    {
        _configuration.GetSection("Cloudinary").Bind(options);
    }
}
