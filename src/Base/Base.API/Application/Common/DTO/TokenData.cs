using System.Text.Json.Serialization;

namespace Shared.Lib.DTO;

public class TokenData
{
    public string? AccessToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
    public string? RefreshToken { get; set; }
    [JsonIgnore]
    public DateTime RefreshTokenExpiresAt { get; set; }
}