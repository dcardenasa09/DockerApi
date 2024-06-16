using MediatR;
using Base.Domain.AggregatesModel.DomainEvents;
using Base.Domain.AggregatesModel.SavingAccountAggregate;
using Base.API.Application.Common.Enums;

namespace Base.API.Application.DomainEvents;

public class TransactionCreatedDomainEventHandler(ISavingAccountRepository savedAccountRepository) : INotificationHandler<TransactionCreatedDomainEvent> {

    private readonly ISavingAccountRepository _savedAccountRepository = savedAccountRepository;

    public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken) {
        SavingAccount? account = await _savedAccountRepository.GetById(notification.SavingAccountId);
        if(account != null) {
            account.Balance += notification.Type == (int)TransactionTypesEnum.Deposit ? notification.Amount : (Math.Abs(notification.Amount) * (-1));
            await _savedAccountRepository.Update(account);
        }
    }
}