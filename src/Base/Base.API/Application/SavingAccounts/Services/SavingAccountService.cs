using AutoMapper;
using Shared.Lib.Helpers;
using Shared.Lib.Services;
using Shared.Lib.Exceptions;
using System.Linq.Expressions;
using Base.API.Application.Common.Enums;
using Base.API.Application.SavingAccounts.Domain.DTO;
using Base.Domain.AggregatesModel.TransactionAggregate;
using Base.Domain.AggregatesModel.SavingAccountAggregate;

namespace Base.API.Application.SavingAccounts.Services;

public class SavingAccountService(IMapper mapper, ISavingAccountRepository SavingAccountRepository) : BaseService<SavingAccount, SavingAccountDTO, ISavingAccountRepository>(mapper, SavingAccountRepository), ISavingAccountService {

    private readonly IMapper _mapper = mapper;
    private readonly ISavingAccountRepository _savingAccountRepository = SavingAccountRepository;

    public async Task<List<SavingAccountDTO>> GetByClientId(int id) {
        var includes = IncludesHelper.GetIncludes<SavingAccount>(x => x.Client);
        List<SavingAccount> response = await _savingAccountRepository.GetList(x => x.IsActive && x.ClientId == id, includes) ?? throw new NotFoundException();

        return response.Count == 0 ? [] : _mapper.Map<List<SavingAccountDTO>>(response);
    }

    public override async Task<List<SavingAccountDTO>> GetList(Expression<Func<SavingAccount, bool>> predicate, string[]? includes = null, bool applyAsNoTracking = true) {
        includes = ["Client"];
        List<SavingAccount> response = await _savingAccountRepository.GetList(x => x.IsActive, includes) ?? throw new NotFoundException();

        return response.Count == 0 ? [] : _mapper.Map<List<SavingAccountDTO>>(response);
    }
}