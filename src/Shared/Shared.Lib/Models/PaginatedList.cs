namespace Shared.Lib.Models;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public int PreviousPage { get; set; }
    public bool HasNextPage => PageNumber < TotalPages;
    public int NextPage { get; set; }
    public string? SortParam { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }

    public PaginatedList(List<T> items, int pageSize, int pageNumber, string? sortParam, bool isDescending, int totalItems)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;

        if (!string.IsNullOrEmpty(sortParam)) {
            if (!isDescending) {
                items = items.OrderBy(x => x?.GetType().GetProperty(sortParam)).ToList();
            } else {
                items = items.OrderByDescending(x => x?.GetType().GetProperty(sortParam)).ToList();
            }
        }

        Items = items;
        TotalItems = totalItems;
        TotalPages =  (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}