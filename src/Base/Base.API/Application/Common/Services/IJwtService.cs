using System.Security.Claims;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.API.Application.Common.Services;

public interface IJwtService {
    string GenerateAccessToken(User user);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}