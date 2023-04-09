using Domain.Entities;
using Fitsou.API.CommonAPI;
using Fitsou.API.Contracts;
using Fitsou.Application.AuthenticationApp;
using Fitsou.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Fitsou.API.AuthenticationAPI;

[AllowAnonymous]
public class AuthenticationController : APIController
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto request)
    {
        var isValid = HttpContext.Items["User"];


        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            return BadRequest(LoginResponseDto.Create("Username and password are required"));

        var response = _authenticationService.Login(LoginRequest.Create(request.UserName, request.Password));

        if (response.IsFailure)
            return BadRequest(LoginResponseDto.Create("login failed"));

        Response.Cookies.Append("at", response.Value, _authenticationService.SetCookieOptions(response.Value));

        return Ok(LoginResponseDto.Create("login success"));
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequestDto request)
    {
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Username and password are required");

        var result = _authenticationService.Register(RegisterRequest.Create(request.UserName, request.Password, request.FirstName, request.LastName, request.Email));

        return Ok(result);
    }



    private RefreshTokenEntity GetRefreshToken()
    {
        return new RefreshTokenEntity()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpireDate = DateTime.Now.AddDays(7),
            CreationDate = DateTime.Now
        };
    }

    private void SetRefreshToken(RefreshTokenEntity newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.ExpireDate
        };

        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        //set user?
    }
}