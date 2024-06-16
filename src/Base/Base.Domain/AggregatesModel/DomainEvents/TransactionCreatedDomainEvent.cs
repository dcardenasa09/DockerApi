using MediatR;

namespace Base.Domain.AggregatesModel.DomainEvents;

public class TransactionCreatedDomainEvent(int savingAccountId, decimal amount, int type) : INotification {

    public int SavingAccountId { get; set; } = savingAccountId;
    public decimal Amount { get; set; } = amount;
    public int Type { get; set; } = type;
}