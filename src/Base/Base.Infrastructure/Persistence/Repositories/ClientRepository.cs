
using Shared.Lib.Repositories;
using Base.Infrastructure.Persistence;
using Base.Domain.AggregatesModel.ClientAggregate;

namespace Base.Infrastructure.Persistence.Repositories;

public class ClientRepository(BaseDbContext context) : BaseRepository<Client>(context), IClientRepository {
    private readonly BaseDbContext _context = context;
}