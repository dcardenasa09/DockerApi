namespace Shared.Lib.DTO;

public class MailRequestDTO {
	public int Id { get; set; }
    public string? ClientName { get; set; }
    public string? Username { get; set; }
    public string? Alias { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}