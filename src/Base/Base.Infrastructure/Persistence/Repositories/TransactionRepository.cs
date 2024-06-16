
using Shared.Lib.Repositories;
using Base.Infrastructure.Persistence;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.Infrastructure.Persistence.Repositories;

public class TransactionRepository(BaseDbContext context) : BaseRepository<Transaction>(context), ITransactionRepository {
    private readonly BaseDbContext _context = context;
}