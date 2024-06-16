namespace Shared.Lib.Models;

public class PaginationFilter {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortParam { get; set; }
    public bool IsDescending { get; set; }
	public bool FilterActives { get; set; } = true;
}