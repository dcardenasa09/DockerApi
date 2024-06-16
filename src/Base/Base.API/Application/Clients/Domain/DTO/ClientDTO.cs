using AutoMapper;
using Shared.Lib.Mapping;
using Shared.Lib.Interfaces;
using Base.Domain.AggregatesModel.ClientAggregate;

namespace Base.API.Application.Clients.Domain.DTO;

public class ClientDTO : IDTO, IMapFrom {

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? SecondLastName { get; set; }
    public string? Phone  { get; set; }
    public string? Email  { get; set; }
    public DateTime Birthdate  { get; set; }
    public bool IsActive { get; set; }

    public void Mapping(Profile profile) {
        profile.CreateMap<Client, ClientDTO>().ReverseMap();
    }
}