namespace Shared.Lib.Models;

public class ErrorResponse {
    public int Code { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public bool HasErrors { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}