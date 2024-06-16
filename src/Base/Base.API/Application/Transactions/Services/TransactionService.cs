using MediatR;
using AutoMapper;
using Shared.Lib.Helpers;
using Shared.Lib.Services;
using Shared.Lib.Exceptions;
using Base.API.Application.Common.Enums;
using Base.API.Application.Transactions;
using Base.API.Application.Transactions.Domain.DTO;
using Base.Domain.AggregatesModel.TransactionAggregate;
using Base.Domain.AggregatesModel.DomainEvents;

namespace Base.API.Application.Transactions.Services;

public class TransactionService(IMapper mapper, IMediator mediator, ITransactionRepository transactionRepository) : BaseService<Transaction, TransactionDTO, ITransactionRepository>(mapper, transactionRepository), ITransactionService {

    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public override async Task<TransactionDTO> Create(TransactionDTO dto) {
        dto.Folio = await GetFolio();
        Transaction transaction = await _transactionRepository.Create(_mapper.Map<Transaction>(dto));

        await _mediator.Publish(new TransactionCreatedDomainEvent(transaction.SavingAccountId,
                                                                  transaction.Amount,
                                                                  transaction.Type));
        return _mapper.Map<TransactionDTO>(transaction);
    }

    private async Task<string> GetFolio() {
       	int numero = 1;

		List<Transaction> transactions = await _transactionRepository.GetList(x => x.IsActive);
        if (transactions.Count > 0) {
            var transaction = transactions.OrderByDescending(x => x.Date).First();

            var x  = transaction.Folio?.Split("TRS-", ',');
            numero = Convert.ToInt32(x?[1]) + 1;
        }

        string folio = string.Format("TRS-{0}", numero);
        return folio;
    }
}