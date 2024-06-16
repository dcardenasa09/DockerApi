
using Shared.Lib.Services;
using Base.API.Application.Clients.Domain.DTO;
using Base.Domain.AggregatesModel.ClientAggregate;

namespace Base.API.Application.Clients.Services;

public interface IClientService: IBaseService<Client, ClientDTO> { }