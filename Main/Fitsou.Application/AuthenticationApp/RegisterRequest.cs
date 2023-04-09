namespace Fitsou.Application.AuthenticationApp;

public record RegisterRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }

    protected RegisterRequest(string username, string password, string firstName, string lastName, string email)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static RegisterRequest Create(string username, string password, string firstName, string lastName, string email)
    {
        return new(username, password, firstName, lastName, email);
    }
}