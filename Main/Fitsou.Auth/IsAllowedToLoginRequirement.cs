using Microsoft.AspNetCore.Authorization;

namespace Auth;

public class IsAllowedToLoginRequirement : IAuthorizationRequirement
{
    public string Username { get; set; }
    public string Password { get; set; }

    public IsAllowedToLoginRequirement(string username, string password)
    {
        Username = username;
        Password = password;
    }
}