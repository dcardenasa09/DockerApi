using Shared.Lib.Services;
using Base.Domain.AggregatesModel.UserAggregate;
using Base.API.Application.Users.Domain.DTO;

namespace Base.API.Application.Users.Services;

public interface IUserService : IBaseService<User, UserDTO> {
    Task<UserDTO> CreateUser(UserDTO userDto);
}