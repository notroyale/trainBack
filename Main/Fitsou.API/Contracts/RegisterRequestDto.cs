namespace Fitsou.API.Contracts;

public record RegisterRequestDto(string UserName, string Email, string FirstName, string LastName, string Password);