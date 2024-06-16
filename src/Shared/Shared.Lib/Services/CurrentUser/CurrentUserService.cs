using Microsoft.AspNetCore.Http;

namespace Shared.Lib.Services.CurrentUser;

public class CurrentUserService : ICurrentUserService {

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
    public string? Token  => _httpContextAccessor.HttpContext?.Items["token"]?.ToString();
}