using AutoMapper;
using Shared.Lib.Mapping;
using Shared.Lib.Interfaces;
using Base.API.Application.Clients.Domain.DTO;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.API.Application.SavingAccounts.Domain.DTO;

public class SavingAccountDTO : IDTO, IMapFrom {
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; } = 0;
    public DateTime OpeningDate { get; set; }
    public bool IsActive { get; set; } = true;

    public ClientDTO? Client { get; set; }

    public void Mapping(Profile profile) {
        profile.CreateMap<SavingAccount, SavingAccountDTO>().ReverseMap();
    }
}