
using Shared.Lib.Interfaces;
using Base.Domain.AggregatesModel.ClientAggregate;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.Domain.AggregatesModel.SavingAccountAggregate;

public class SavingAccount : IEntity {
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpeningDate { get; set; }
    public bool IsActive { get; set; }

    public virtual Client? Client { get; set; }
    public virtual List<Transaction>? Transactions { get; set; }
}