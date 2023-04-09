using Fitsou.Application.UserApp;
using Auth;
using Fitsou.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fitsou.API.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSettings _jwtSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
    {
        _next = next;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //var token = context.Request.Cookies["at"];


        //if (token != null)
        //    attachUserToContext(context, userService, token);

        await _next(context);
    }

    private void attachUserToContext(HttpContext context, IUserService userService, string token)
    {
        //try
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    tokenHandler.ValidateToken(token, new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //        ClockSkew = TimeSpan.Zero
        //    }, out SecurityToken validatedToken);

        //    var jwtToken = (JwtSecurityToken)validatedToken;
        //    var userId = jwtToken.Claims.First(x => x.Type == "sub").Value;

        //    // attach user to context on successful jwt validation
        //    context.Items["User"] = userService.GetById(userId);
        //}
        //catch
        //{
        //    // do nothing if jwt validation fails
        //    // user is not attached to context so request won't have access to secure routes
        //}
    }
}