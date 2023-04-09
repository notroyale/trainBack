namespace Auth;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public const string AuthCookieName = "jac";
    public string Secret { get; init; } = null!;
    public int ExpirationTimeInMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}