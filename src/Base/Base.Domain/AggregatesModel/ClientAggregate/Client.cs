
using Shared.Lib.Interfaces;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.Domain.AggregatesModel.ClientAggregate;

public class Client : IEntity {

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? SecondLastName { get; set; }
    public string? Phone  { get; set; }
    public string? Email  { get; set; }
    public DateTime Birthdate  { get; set; }
    public bool IsActive { get; set; }

    public virtual List<SavingAccount>? SavingAccounts { get; set; }

    public string GetFullName() {
        return $"{Name} {LastName} {SecondLastName}";
    }
}