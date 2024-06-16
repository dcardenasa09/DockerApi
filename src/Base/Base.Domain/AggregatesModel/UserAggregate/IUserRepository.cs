using Shared.Lib.Repositories;

namespace Base.Domain.AggregatesModel.UserAggregate;

public interface IUserRepository : IBaseRepository<User> {

    Task SetRefreshToken(int id, string refreshToken, DateTime expiresAt);
	Task RevokeRefreshToken(int id);  
}