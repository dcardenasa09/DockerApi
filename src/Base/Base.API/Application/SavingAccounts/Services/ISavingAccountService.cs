using Shared.Lib.Services;
using Base.API.Application.SavingAccounts.Domain.DTO;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.API.Application.SavingAccounts.Services;

public interface ISavingAccountService: IBaseService<SavingAccount, SavingAccountDTO> {
    Task<List<SavingAccountDTO>> GetByClientId(int id);
}