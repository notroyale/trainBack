using Fitsou.Application.AuthenticationApp;
using Messaging;
using Microsoft.AspNetCore.Http;

namespace Fitsou.Application.Contracts;

public interface IAuthenticationService
{
    Result<string> Login(LoginRequest request);
    Result<AuthenticationResult> Register(RegisterRequest request);
    CookieOptions SetCookieOptions(string jwtToken);
}