using Shared.Lib.Repositories;
using Base.Infrastructure.Persistence;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.Infrastructure.Persistence.Repositories;

public class SavingAccountRepository(BaseDbContext context) : BaseRepository<SavingAccount>(context), ISavingAccountRepository {
    private readonly BaseDbContext _context = context;
}