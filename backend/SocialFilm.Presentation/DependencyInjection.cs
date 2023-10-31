using Microsoft.Extensions.DependencyInjection;

using SocialFilm.Presentation.Middlewares;

namespace SocialFilm.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<ExceptionMiddleware>();

        return services;
    }
}