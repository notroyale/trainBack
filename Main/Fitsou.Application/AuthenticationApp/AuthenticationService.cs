using Domain.Entities;
using Fitsou.Application.Common;
using Fitsou.Application.Contracts;
using Messaging;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Fitsou.Application.AuthenticationApp;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserService _userService;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserService userService)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userService = userService;
    }

    public Result<AuthenticationResult> Register(RegisterRequest newUser)
    {
        if (CheckIfUserExists(newUser.Username))
        {
            return ApplicationErrors.Register.Conflict;
        }

        string salt = GenerateSalt();
        string passwordHash = GeneratePasswordHash(newUser.Password, salt);

        _userService.Add(new UserEntity(salt)
        {
            UserName = newUser.Username,
            Email = newUser.Email,
            PasswordHash = passwordHash
        });

        Guid userID = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userID.ToString(), newUser.FirstName);

        return new AuthenticationResult(userID, newUser.FirstName, newUser.LastName, newUser.Email, token);
    }

    public Result<string> Login(LoginRequest request)
    {
        var user = _userService.GetByUsername(request.Username);
        if (user == null)
        {
            return ApplicationErrors.Login.NotFound;
        }

        string salt = user.Salt;// GetSaltForUser(request.Username);
        string passwordHash = user.PasswordHash;// GetPasswordHashForUser(request.Username);

        if (string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(passwordHash))
        {
            return ApplicationErrors.Login.Unauthorized;
        }

        string hashedPassword = GeneratePasswordHash(request.Password, salt);

        if (hashedPassword != passwordHash)
        {
            return ApplicationErrors.Login.Unauthorized;
        }

        return _jwtTokenGenerator.GenerateToken(user.Id, user.UserName);
    }
    public CookieOptions SetCookieOptions(string jwtToken)
    {
        var options = new CookieOptions
        {
            HttpOnly = true, // set the HTTP-only flag
            Secure = true, // set the secure flag to ensure that the cookie is only transmitted over HTTPS
            SameSite = SameSiteMode.Strict, // set the SameSite attribute to Strict to prevent cross-site request forgery (CSRF) attacks
            Expires = DateTime.UtcNow.AddHours(1) // set the expiration time for the cookie
        };

        return options;
    }

    private string GenerateSalt()
    {
        int saltSize = 16; // Size of the salt in bytes

        byte[] saltBytes = new byte[saltSize];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes); // Generate a random salt
        }

        return Convert.ToBase64String(saltBytes); // Return the Base64-encoded salt
    }

    private string GeneratePasswordHash(string password, string salt)
    {
        int iterationCount = 10000; // Number of iterations for the key derivation function
        int hashSize = 64; // Size of the output hash in bytes

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Convert.FromBase64String(salt);

        byte[] hashBytes;
        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iterationCount))
        {
            hashBytes = pbkdf2.GetBytes(hashSize); // Generate the hashed password
        }

        byte[] saltedHashBytes = new byte[hashSize + saltBytes.Length]; // Combine the hash and salt into a single byte array
        Array.Copy(hashBytes, 0, saltedHashBytes, 0, hashSize);
        Array.Copy(saltBytes, 0, saltedHashBytes, hashSize, saltBytes.Length);

        return Convert.ToBase64String(saltedHashBytes); // Return the Base64-encoded salted hash
    }

    private bool CheckIfUserExists(string username)
    {

        return false;
    }





}