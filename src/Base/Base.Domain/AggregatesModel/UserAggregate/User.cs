using Shared.Lib.Interfaces;

namespace Base.Domain.AggregatesModel.UserAggregate;

public class User : IEntity {
    public int Id { get; set; }
	public string? Name { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public string? AccessToken { get; set; }
	public DateTime AccessTokenExpiresAt { get; set; }
	public string? RefreshToken { get; set; }
	public DateTime RefreshTokenExpiresAt { get; set; }
}