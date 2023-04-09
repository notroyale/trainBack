using Microsoft.Extensions.Options;

namespace Fitsou.API.SwaggerAPI;

public static class SwaggerDependencyInjection
{
    public static void AddSwagger(this IServiceCollection services, ConfigurationManager configuration)
    {
        var bearerScheme = new BearerOpenApiSecurityScheme();

        configuration.Bind(BearerOpenApiSecurityScheme.SectionName, bearerScheme);

        services.AddSingleton(Options.Create(bearerScheme));

        services.AddSwaggerGen(options => options.AddBearerSecurity(bearerScheme));
    }
}