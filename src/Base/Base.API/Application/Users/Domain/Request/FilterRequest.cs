namespace Base.API.Application.Users.Domain.Request;

public class FilterRequest {
    public string? CompanyId { get; set; }
	public string? DepartmentId { get; set; }
    public string? RoleId { get; set; }
	public string? Search { get; set; }
    public string[]? DepartmentIds { get; set; }
    public string[]? EmployeesId { get; set; }
}