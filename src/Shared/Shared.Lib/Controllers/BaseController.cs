using Shared.Lib.Models;
using Shared.Lib.Helpers;
using Shared.Lib.Services;
using Shared.Lib.Interfaces;
using Shared.Lib.Exceptions;
using Shared.Lib.Helpers.Validator;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Shared.Lib.Controllers;

[Route("api/[controller]")]
[ApiController]

public abstract class BaseController<TEntity, TDTO, TService>(TService service, IValidatorHelper<TDTO> validator) : ControllerBase where TEntity : IEntity where TDTO : IDTO where TService : IBaseService<TEntity, TDTO> {

    private readonly TService _service = service;
    private readonly IValidatorHelper<TDTO> _validator = validator;

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Get([FromQuery] string[]? includes = null) {
        Expression<Func<TEntity, bool>> expression = t => true;
        var entities = await _service.GetList(expression, includes);
        if (entities == null) {
            return NotFound();
        }

        return Ok(entities);
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<TDTO>> Get(int id, [FromQuery] string[]? includes = null) {
        var entity = await _service.GetById(id, includes);
        if (entity == null) {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public virtual async Task<ActionResult<TDTO>> Create(TDTO entity) {
        await _validator.Validate(entity);
        var entityResult = await _service.Create(entity);
        return CreatedAtAction(null, entityResult);
    }


    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put(TDTO entity) {
        await _validator.Validate(entity);
        var result = await _service.Update(entity);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<bool>> Delete(int id) {
        await _service.Delete(id);
        return Ok();
    }
}