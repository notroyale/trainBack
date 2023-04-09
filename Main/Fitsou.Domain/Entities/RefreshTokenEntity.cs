namespace Domain.Entities;

public class RefreshTokenEntity
{
    public string Token { get; set; } = string.Empty;
    public string JwtId { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public bool Used { get; set; }
    public bool Invalidated { get; set; }
    public bool UserId { get; set; }
    public UserEntity User { get; set; } = default!;
}