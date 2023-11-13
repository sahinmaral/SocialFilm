using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SocialFilm.Infrastructure.FileStorage;


public sealed class CloudinaryOptions
{
    public string CloudName { get; set; } = null!;
    public string APIKey { get; set; } = null!;
    public string APISecret { get; set; } = null!;
}
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
