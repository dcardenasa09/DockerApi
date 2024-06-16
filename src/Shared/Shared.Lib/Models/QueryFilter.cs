namespace Shared.Lib.Models;

public class QueryFilter {
    public string? PropertyName { get; set; }
    public int Operation { get; set; }
    public string? Value { get; set; }
    public bool IsHashed { get; set; }
}