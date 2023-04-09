using Fitsou.Application.AuthenticationApp;
using Fitsou.Application.Contracts;
using Fitsou.Application.UserApp;
using Microsoft.Extensions.DependencyInjection;

namespace Fitsou.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IUserService, UserService>();


        return services;
    }
}