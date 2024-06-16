using Shared.Lib.Repositories;
using Microsoft.EntityFrameworkCore;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.Infrastructure.Persistence.Repositories;

public class UserRepository(BaseDbContext context) : BaseRepository<User>(context), IUserRepository {

    private readonly BaseDbContext _context = context;

    public async Task SetRefreshToken(int id, string refreshToken, DateTime expiresAt) {
		User user = new() {Id = id, RefreshToken = refreshToken, RefreshTokenExpiresAt = expiresAt};

		_context.Entry(user).Property(x => x.RefreshToken).IsModified          = true;
		_context.Entry(user).Property(x => x.RefreshTokenExpiresAt).IsModified = true;

		await _context.SaveChangesAsync();
		_context.ChangeTracker.Clear();
	}

    public async Task RevokeRefreshToken(int id) {
		User user = new() {Id = id, RefreshToken = string.Empty};

		_context.Entry(user).State = EntityState.Detached;
		_context.Entry(user).Property(x => x.RefreshToken).IsModified = true;

		await _context.SaveChangesAsync();
	}
}