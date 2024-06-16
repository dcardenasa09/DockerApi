using System.Text;
using Shared.Lib.Models;
using Shared.Lib.Helpers;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.API.Application.Common.Services;

public interface IJwtTokenGenerator {
    string GenerateToken(User user,string[] department);
}

public class JwtTokenGenerator(IOptions<AppSettings> appSettings) : IJwtTokenGenerator {
    private readonly AppSettings _appSettings = appSettings.Value;

    public string GenerateToken(User user,string[] department) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity([
                new Claim("id", user.Id.ToString()),
                new Claim("languageCode", "es"),
            ]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}