using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.FileStorage;
using SocialFilm.Infrastructure.ApiClients;
using SocialFilm.Infrastructure.Authentication.OptionsSetup;
using SocialFilm.Infrastructure.FileStorage;

namespace SocialFilm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITMDBApiClient, TMDBApiClient>();
        services.AddSingleton<ICloudinaryService, CloudinaryService>();

        services.ConfigureOptions<CloudinaryOptionsSetup>();
        services.ConfigureOptions<JWTOptionsSetup>();
        services.ConfigureOptions<JWTBearerOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();

        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();

        services.ConfigureApplicationCookie(o => {
            o.ExpireTimeSpan = TimeSpan.FromDays(5);
            o.SlidingExpiration = true;
        });

        services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromHours(3));

        return services;
    }
}