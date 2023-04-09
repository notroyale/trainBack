using Domain.Entities;
using Fitsou.Application.AuthenticationApp;
using Fitsou.Application.Common;
using Fitsou.Application.Contracts;
using Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions, TokenValidationParameters tokenValidationParameters)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public string GenerateToken(string userID, string userName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)), 
                SecurityAlgorithms.HmacSha512Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, $"{Guid.NewGuid()}" ),
            new Claim(JwtRegisteredClaimNames.Sub, $"{userID}" ),
            new Claim(JwtRegisteredClaimNames.UniqueName, $"{userID}" ),
            new Claim(JwtRegisteredClaimNames.GivenName, $"{userName}" ),
            new Claim(JwtRegisteredClaimNames.FamilyName, $"{userName}" )
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes), // <---- need fix
            claims: claims, signingCredentials: signingCredentials);

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);


        //create refresh token

        //add to db
        //return result

        return token;
    }

    


    public Result<ClaimsPrincipal> GetPrincipalFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        try
        {
            var user = handler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                return ApplicationErrors.RefreshToken.InvalidToken;

            return user;
        }
        catch (Exception)
        {
            return ApplicationErrors.RefreshToken.InvalidToken;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) && 
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }

   
}