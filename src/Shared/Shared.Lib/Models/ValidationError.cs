namespace Shared.Lib.Models;

public class ValidationError {
    public string? PropertyName { get; set; }
    public string? FormName { get; set; }
    public string[]? Messages { get; set; }
}