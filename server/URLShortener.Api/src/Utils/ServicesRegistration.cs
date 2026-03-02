using URLShortener.Api.Repositories;
using URLShortener.Api.Services;

namespace URLShortener.Api.Utils;

public static class ServicesRegistration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUrlRepository, UrlRepository>();
        services.AddScoped<IUrlService, UrlService>();
        services.AddScoped<IHashService, Base62Service>();
        return services;
    }
    
}
