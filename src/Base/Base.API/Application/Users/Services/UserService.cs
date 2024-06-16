using AutoMapper;
using Shared.Lib.Services;
using Shared.Lib.Exceptions;
using Base.API.Application.Users.Domain.DTO;
using Base.Domain.AggregatesModel.UserAggregate;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Base.API.Application.Users.Services;

public class UserService(IMapper mapper, IUserRepository userRepository) : BaseService<User, UserDTO, IUserRepository>(mapper, userRepository), IUserService {

    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDTO> CreateUser(UserDTO userDto) {
        User? existingUser = await _userRepository.GetFirst(x => x.Email == userDto.Email, null, false);
        if(existingUser != null) {
            throw new AppException("El usuario con el correo ya fue registrado");
        }

        userDto.Password = BCryptNet.HashPassword(userDto.Password);

        var entity      = _mapper.Map<User>(userDto);
        var savedEntity = await _userRepository.Create(entity);
        return _mapper.Map<UserDTO>(savedEntity);
    }

    public override async Task<UserDTO> Update(UserDTO userDto) {
        User entity = await _userRepository.GetById(userDto.Id) ?? throw new AppException("No se encuentra el usuario");

        entity.Name     = userDto.Name;
        entity.LastName = userDto.LastName;
        entity.Email    = userDto.Email;

        if (!string.IsNullOrEmpty(userDto.Password)) {
            entity.Password = BCryptNet.HashPassword(userDto.Password);
        }

        await _userRepository.Update(entity);
        return userDto;
    }
}