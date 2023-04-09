namespace Fitsou.API.Contracts;

public record LoginResponseDto
{
    public string Response { get; init; }

    protected LoginResponseDto(string response)
    {
        Response = response;
    }

    public static LoginResponseDto Create(string response)
    {
        return new LoginResponseDto(response);
    }
}