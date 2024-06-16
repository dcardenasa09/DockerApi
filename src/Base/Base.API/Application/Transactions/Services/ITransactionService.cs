using Shared.Lib.Services;
using Base.API.Application.Transactions.Domain.DTO;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.API.Application.Transactions.Services;

public interface ITransactionService: IBaseService<Transaction, TransactionDTO> {}