using System.Text;
using Shared.Lib.Models;
using Shared.Lib.Helpers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Base.Domain.AggregatesModel.UserAggregate;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.API.Application.Common.Services;

public class JwtService(IOptions<AppSettings> appSettings) : IJwtService {

	private readonly AppSettings _appSettings = appSettings.Value;

    public string GenerateAccessToken(User user) {
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

		List<Claim> claims = BuildClaims(user);

		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject            = new ClaimsIdentity(claims),
			Expires            = DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenValidityInMinutes),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	private List<Claim> BuildClaims(User user) {
		List<Claim> claims = [
			new Claim("id", user.Id.ToString()),
			new Claim("accessTokenExpiresAt", DateTime.UtcNow.AddMinutes(_appSettings.AccessTokenValidityInMinutes).ToString())
		];

		return claims;
	}

	public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token) {
		var key = Encoding.ASCII.GetBytes(_appSettings.Secret ?? "");
		var issuer   = _appSettings.Issuer;
		var audience = _appSettings.Audience;

		var tokenValidationParameters = new TokenValidationParameters {
			IssuerSigningKey         = new SymmetricSecurityKey(key),
			ValidAudience            = audience,
			ValidIssuer              = issuer,
			ValidateIssuer           = false,
			ValidateAudience         = false,
			ValidateLifetime         = false,
			ValidateIssuerSigningKey = true,
			ClockSkew                = TimeSpan.Zero
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var principal    = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

		if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
			throw new SecurityTokenException("Invalid token");
		}

		return principal;
	}
}