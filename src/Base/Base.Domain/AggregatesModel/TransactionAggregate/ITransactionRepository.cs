using Shared.Lib.Repositories;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.Domain.AggregatesModel.TransactionAggregate;

public interface ITransactionRepository: IBaseRepository<Transaction> {}