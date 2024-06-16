using System.Text;
using Shared.Lib.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Shared.Lib.Middlewares;

public class JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings) {

	private readonly RequestDelegate _next = next;
	private readonly AppSettings _appSettings = appSettings.Value;

    public async Task Invoke(HttpContext context) {
		var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

		if (token != null) {
			JwtSecurityToken validatedToken = ValidateToken(token);
			await AttachUserDataToContext(context, validatedToken, token);
		}

		await _next(context);
	}

	private JwtSecurityToken ValidateToken(string token) {
		if (token == null)
			throw new UnauthorizedAccessException();

		var tokenHandler = new JwtSecurityTokenHandler();
		var key          = Encoding.ASCII.GetBytes(_appSettings.Secret);
		var issuer       = _appSettings.Issuer;
		var audience     = _appSettings.Audience;

		try {
			tokenHandler.ValidateToken(token, new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey         = new SymmetricSecurityKey(key),
				ValidateIssuer           = false,
				ValidIssuer              = issuer,
				ValidateAudience         = false,
				ValidAudience            = audience,
				ValidateLifetime         = false,
				ClockSkew                = TimeSpan.Zero,
			}, out SecurityToken validatedToken);

			return (JwtSecurityToken)validatedToken;
		} catch(Exception ex) {
			throw new UnauthorizedAccessException(ex.Message);
		}
	}

	private static async Task AttachUserDataToContext(HttpContext context, JwtSecurityToken validatedToken, string token) {
		var jwtToken = validatedToken;
		var userId   = jwtToken.Claims.First(x => x.Type == "id").Value;

		string headers = string.Empty;
		if(context.Request.Headers != null) {
			foreach(var header in context.Request.Headers) {
				headers += $"Key: {header.Key}, Value: {header.Value}, ";
			}
		}

		context.Request.EnableBuffering();
		context.Items["body"] = string.Empty;
		using var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
		if(context.Request.Body.CanRead){
			string bodyAsString = await reader.ReadToEndAsync();
			context.Request.Body.Position = 0;
			context.Items["body"] = bodyAsString;
		}

		context.Items["UserId"]  = userId;
		context.Items["token"]   = token;
		context.Items["headers"] = headers;
	}
}