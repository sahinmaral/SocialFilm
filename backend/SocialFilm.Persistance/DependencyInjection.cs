using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SocialFilm.Application.Abstractions;
using SocialFilm.Application.Services;
using SocialFilm.Domain.Entities;
using SocialFilm.Domain.Repositories;
using SocialFilm.Infrastructure.Identity;
using SocialFilm.Infrastructure.Repositories;
using SocialFilm.Persistance.Context;
using SocialFilm.Persistance.Services;

namespace SocialFilm.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQLConnectionString");

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IFilmDetailRepository, FilmDetailRepository>();
        services.AddScoped<ISavedFilmRepository, SavedFilmRepository>();

        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IFilmDetailService, FilmDetailService>();
        services.AddScoped<ISavedFilmService, SavedFilmService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}