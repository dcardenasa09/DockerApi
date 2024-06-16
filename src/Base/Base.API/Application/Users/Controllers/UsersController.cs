using Shared.Lib.Controllers;
using Shared.Lib.Helpers.Validator;
using Microsoft.AspNetCore.Mvc;
using Base.Domain.AggregatesModel.UserAggregate;
using Base.API.Application.Users.Services;
using Base.API.Application.Users.Domain.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Base.API.Application.Users.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService, IAuthService authService, IValidatorHelper<UserDTO> validator) : BaseController<User, UserDTO, IUserService>(userService, validator) {

    private readonly IUserService _userService = userService;
    private readonly IAuthService _authService = authService;

    [HttpPost("Login")]
    public async Task<ActionResult> Login(AuthenticateRequest credentials) {
        AuthenticateResponse authResponse = await _authService.Authenticate(credentials);
        return Ok(authResponse);
    }

    [HttpPost]
    [AllowAnonymous]
    public override async Task<ActionResult<UserDTO>> Create(UserDTO entity) {
        var response = await _userService.CreateUser(entity);
        return Ok(response);
    }
}
