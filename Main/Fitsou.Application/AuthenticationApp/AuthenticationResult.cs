namespace Fitsou.Application.AuthenticationApp;

public class AuthenticationResult
{
    public Guid UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public AuthenticationResult(Guid userID, string firstName, string lastName, string email, string token)
    {
        UserID = userID;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Token = token;
    }
}