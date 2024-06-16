namespace Base.API.Application.Users.Domain.DTO;

public class UserTokenDTO
{
    public string? AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? RefreshToken { get; set; }
}