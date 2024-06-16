using Base.API.Application.Users.Domain.DTO;

namespace Base.API.Application.Users.Services;

public interface IAuthService {
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
}