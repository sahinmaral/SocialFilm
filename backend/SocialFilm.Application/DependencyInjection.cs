using Microsoft.Extensions.DependencyInjection;

namespace SocialFilm.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
        });
        
        services.AddAutoMapper(typeof(AssemblyReference).Assembly);

        return services;
    }
}