using Messaging;

namespace Fitsou.Application.Common;

public static class ApplicationErrors
{
    public static class Login
    {
        public static readonly Error Unauthorized = new("Login.Unauthorized", "User does not have permission!");
        public static readonly Error NotFound = new("Login.NotFound", "User not found!");
    }

    public static class Register
    {
        public static readonly Error Conflict = new("Register.Conflict", "Username already exists");
    }

    public static class RefreshToken
    {
        public static readonly Error InvalidToken = new("JwtTokenGenerator.RefreshToken", "Jwt token has an invalid security algorithm!");
        public static readonly Error TokenHasNotExpired = new("JwtTokenGenerator.RefreshToken", "Jwt token has not expired yet!");
        public static readonly Error TokenHasExpired = new("JwtTokenGenerator.RefreshToken", "Jwt token has expired!");
        public static readonly Error TokenNotFound = new("JwtTokenGenerator.RefreshToken", "Jwt token not found!");
        public static readonly Error TokenInvalidated = new("JwtTokenGenerator.RefreshToken", "Jwt token has been invalidated!");
        public static readonly Error TokenUsed = new("JwtTokenGenerator.RefreshToken", "Jwt token has been used!");
        public static readonly Error TokenWrongID = new("JwtTokenGenerator.RefreshToken", "Jwt token does not match this JWT!");
    }
}