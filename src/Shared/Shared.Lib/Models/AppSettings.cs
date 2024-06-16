namespace Shared.Lib.Models;

public class AppSettings {
    public string? Secret { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int AccessTokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInHours { get; set; }
	public string? FilesPath { get; set; }
	public string? ServerUrl { get; set; }
}