using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fitsou.API.SwaggerAPI;

public static class SwaggerBearerSecurityOptions
{
    public static void AddBearerSecurity(this SwaggerGenOptions options, BearerOpenApiSecurityScheme bearerScheme)
    {
        options.AddSecurityDefinition(bearerScheme.Scheme, new OpenApiSecurityScheme
        {
            Scheme = bearerScheme.Scheme,
            BearerFormat = bearerScheme.BearerFormat,
            In = ParameterLocation.Header,
            Name = bearerScheme.Name,
            Description = bearerScheme.Description,
            Type = SecuritySchemeType.Http
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = bearerScheme.Scheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    }
}