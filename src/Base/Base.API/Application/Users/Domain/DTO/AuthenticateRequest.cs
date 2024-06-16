namespace Base.API.Application.Users.Domain.DTO;

public class AuthenticateRequest {
	public string? Email { get; set; }
	public string? Password { get; set; }
}