namespace Fitsou.Application.AuthenticationApp;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userID, string userName);
}