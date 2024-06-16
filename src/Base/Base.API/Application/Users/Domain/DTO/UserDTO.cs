using AutoMapper;
using Shared.Lib.Mapping;
using Shared.Lib.Interfaces;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.API.Application.Users.Domain.DTO;

public class UserDTO : IDTO, IMapFrom {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; } 
    public string? Email { get; set; }
    public string? Password { get; set; }

    public void Mapping(Profile profile) {
        profile.CreateMap<User, UserDTO>().ReverseMap();
    }
}