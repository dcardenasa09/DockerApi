
using Shared.Lib.Interfaces;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.Domain.AggregatesModel.TransactionAggregate;

public class Transaction : IEntity {
    public int Id { get; set; }
    public string? Folio { get; set; }
    public int SavingAccountId { get; set; }
    public DateTime Date  { get; set; }
    public decimal Amount  { get; set; }
    public int Type  { get; set; }
    public int Status  { get; set; }
    public string? Observations  { get; set; }
    public bool IsActive { get; set; }

    public virtual SavingAccount? SavingAccount { get; set; }
}