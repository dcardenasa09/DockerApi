using AutoMapper;
using Shared.Lib.Services;
using Base.API.Application.Clients.Domain.DTO;
using Base.Domain.AggregatesModel.ClientAggregate;

namespace Base.API.Application.Clients.Services;

public class ClientService(IMapper mapper, IClientRepository ClientRepository) : BaseService<Client, ClientDTO, IClientRepository>(mapper, ClientRepository), IClientService {

    private readonly IMapper _mapper = mapper;
    private readonly IClientRepository _ClientRepository = ClientRepository;
}