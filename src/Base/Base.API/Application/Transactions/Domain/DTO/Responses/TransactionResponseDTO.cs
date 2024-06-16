using AutoMapper;
using Shared.Lib.Mapping;
using Shared.Lib.Interfaces;
using Base.API.Application.Clients.Domain.DTO;
using Base.API.Application.SavingAccounts.Domain.DTO;
using Base.Domain.AggregatesModel.TransactionAggregate;

namespace Base.API.Application.Transactions.Domain.DTO;

public class TransactionResponseDTO {
    public int Id { get; set; }
    public int SavingAccountId { get; set; }
    public string? Folio { get; set; }
    public DateTime Date  { get; set; }
    public decimal Amount  { get; set; }
    public int Type  { get; set; }
    public int Status  { get; set; }
    public string? Observations  { get; set; }
    public bool IsActive { get; set; }

    public SavingAccountDTO? SavingAccount { get; set; }

}