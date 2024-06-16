using Microsoft.AspNetCore.Mvc;
using Base.API.Application.Clients;
using Base.API.Application.Clients.Services;
using Base.API.Application.Clients.Domain.DTO;
using Shared.Lib.Controllers;
using Shared.Lib.Helpers.Validator;
using Base.Domain.AggregatesModel.ClientAggregate;

namespace Base.API.Application.Clients.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService ClientService, IValidatorHelper<ClientDTO> validator) : BaseController<Client, ClientDTO, IClientService>(ClientService, validator) {
    private readonly IClientService _ClientService = ClientService;
}