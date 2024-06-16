using Moq;
using Xunit;
using System;
using AutoMapper;
using BCrypt.Net;
using Shared.Lib.Exceptions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Base.API.Application.Users.Domain;
using Base.API.Application.Users.Services;
using Base.API.Application.Users.Domain.DTO;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.UnitTests.Application.UsersTest;

public class UserServiceTests {

    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserServiceTests() {
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock         = new Mock<IMapper>();
        _userService        = new UserService(_mapperMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateWithEmailAlreadyExistsException() {
        var userDto = new UserDTO { Email = "existing.email@example.com", Password = "password" };
        _userRepositoryMock.Setup(repo => repo.GetFirst(It.IsAny<Expression<Func<User, bool>>>(), null, false))
                           .ReturnsAsync(new User { Email = "existing.email@example.com" });

        var exception = await Assert.ThrowsAsync<AppException>(() => _userService.CreateUser(userDto));
        Assert.Equal("El usuario con el correo ya fue registrado", exception.Message);
    }

    [Fact]
    public async Task CreateWithValidationOfEncryptionPassword() {
        var user      = new User { Email = "new.email@example.com", Password = "hashedPassword" };
        var userDto   = new UserDTO { Email = "new.email@example.com", Password = "password" };
        var savedUser = new User { Id = 1, Email = "new.email@example.com", Password = "hashedPassword" };

        _userRepositoryMock.Setup(repo => repo.GetFirst(It.IsAny<Expression<Func<User, bool>>>(), null, true))
                           .ReturnsAsync((User)null);

        _mapperMock.Setup(m => m.Map<User>(It.IsAny<UserDTO>())).Returns(user);
        _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync(savedUser);
        _mapperMock.Setup(m => m.Map<UserDTO>(It.IsAny<User>())).Returns(userDto);

        var result = await _userService.Create(userDto);

        _userRepositoryMock.Verify(repo => repo.Create(It.Is<User>(u => u.Password != "password")), Times.Once);
        Assert.Equal(userDto.Email, result.Email);
    }
}
