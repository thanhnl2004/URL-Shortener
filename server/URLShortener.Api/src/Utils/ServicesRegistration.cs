using Microsoft.EntityFrameworkCore;
using URLShortener.Api.Persistence;
using URLShortener.Api.Repositories;
using URLShortener.Api.Services;
using URLShortener.Api.Services.HashService;

namespace URLShortener.Api.Utils;

public static class ServicesRegistration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
        services.AddScoped<IShortLinkService, ShortLinkService>();
        services.AddScoped<IHashService, Base62Service>();
        return services;
    }
    
}