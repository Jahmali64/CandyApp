using CandyApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CandyApp.Application;
public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddScoped<IFanService, FanService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
