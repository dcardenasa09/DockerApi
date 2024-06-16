using Shared.Lib.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shared.Lib.Helpers.Validator;
using Base.API.Application.SavingAccounts.Services;
using Base.API.Application.SavingAccounts.Domain.DTO;
using Base.Domain.AggregatesModel.SavingAccountAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Base.API.Application.SavingAccounts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SavingAccountsController(ISavingAccountService SavingAccountService, IValidatorHelper<SavingAccountDTO> validator) : BaseController<SavingAccount, SavingAccountDTO, ISavingAccountService>(SavingAccountService, validator) {
    private readonly ISavingAccountService _savingAccountService = SavingAccountService;

    [HttpGet("ByClient/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<List<SavingAccountDTO>>> GetByClientId([FromRoute] int id) {
        var reponse = await _savingAccountService.GetByClientId(id);
        return Ok(reponse);
    }
}