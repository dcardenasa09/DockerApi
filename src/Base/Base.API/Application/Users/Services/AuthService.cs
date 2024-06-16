using AutoMapper;
using Shared.Lib.DTO;
using Shared.Lib.Models;
using Shared.Lib.Services;
using Shared.Lib.Exceptions;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Base.API.Application.Users.Domain.DTO;
using Base.API.Application.Common.Services;
using Base.Domain.AggregatesModel.UserAggregate;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Base.API.Application.Users.Services;

public class AuthService(IMapper mapper, IUserRepository userRepository, IJwtService jwtService, IOptions<AppSettings> appSettings) : IAuthService {

	private readonly IMapper _mapper = mapper;
	private readonly IJwtService _jwtService = jwtService;
	private readonly IUserRepository _userRepository = userRepository;
	private readonly AppSettings _appSettings = appSettings.Value;

	public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest) {
		var user = await GetUserByUserNameAndPasword(authenticateRequest);
		var response = _mapper.Map<AuthenticateResponse>(user);

		response.AccessToken           = _jwtService.GenerateAccessToken(user);
		response.RefreshToken          = GenerateRefreshToken();
		response.AccessTokenExpiresAt  = DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenValidityInMinutes);
		response.RefreshTokenExpiresAt = DateTime.UtcNow.AddHours(_appSettings.RefreshTokenValidityInHours);

		await ValidateTypeRefreshToken(user.Id, response.RefreshToken, response.RefreshTokenExpiresAt);

		return response;
	}

	private async Task<User> GetUserByUserNameAndPasword(AuthenticateRequest model) {
		var user = await _userRepository.GetFirst(u => u.Email == model.Email) ?? throw new AppException("InvalidUserName");
		if (!BCryptNet.Verify(model.Password, user.Password)) {
			throw new AppException("InvalidPassword");
		}

		return user;
	}

	public async Task<TokenData> RefreshToken(int userId, string accessToken, string refreshToken) {
        var claimPrincipal  = _jwtService.GetPrincipalFromExpiredToken(accessToken);

		bool isValid = await IsValidRefreshToken(userId, refreshToken);
		if(!isValid || claimPrincipal == null) {
			throw new AppException("Invalid token");
		}

		User user = await _userRepository.GetById(userId);
        TokenData tokenData = new() {
            AccessToken           = _jwtService.GenerateAccessToken(user),
            AccessTokenExpiresAt  = DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenValidityInMinutes),
            RefreshToken          = GenerateRefreshToken(),
            RefreshTokenExpiresAt = DateTime.UtcNow.AddHours(_appSettings.RefreshTokenValidityInHours)
        };

        await ValidateTypeRefreshToken(user.Id, tokenData.RefreshToken, tokenData.RefreshTokenExpiresAt);

		return tokenData;
	}

	 private async Task<bool> IsValidRefreshToken(int userId, string refreshToken) {
		bool isValid = false;

		User user = await _userRepository.GetById(userId);
		if (user != null) {
			if(user.RefreshToken == refreshToken && DateTime.UtcNow <= user.RefreshTokenExpiresAt) {
				isValid = true;
			}
		}

		return isValid;
	}

	private async Task ValidateTypeRefreshToken(int id, string refreshToken, DateTime expiresAt) {
		await _userRepository.SetRefreshToken(id, refreshToken, expiresAt);
	}

	public async Task RevokeRefreshToken(int userId) {
		await _userRepository.RevokeRefreshToken(userId);
	}

	public string GenerateRefreshToken() {
		var randomNumber = new byte[64];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}
}