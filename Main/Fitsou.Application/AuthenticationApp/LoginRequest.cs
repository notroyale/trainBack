namespace Fitsou.Application.AuthenticationApp;

public record LoginRequest
{
    public string Username { get; init; }
    public string Password { get; init; }

    protected LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public static LoginRequest Create(string username, string password)
    {
        return new(username, password);
    }
}