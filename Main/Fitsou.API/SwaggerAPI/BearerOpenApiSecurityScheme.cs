namespace Fitsou.API.SwaggerAPI;

public class BearerOpenApiSecurityScheme
{
    public const string SectionName = "Swagger:BearerOpenApiSecurityScheme";
    public string Scheme { get; set; } = null!;
    public string BearerFormat { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}