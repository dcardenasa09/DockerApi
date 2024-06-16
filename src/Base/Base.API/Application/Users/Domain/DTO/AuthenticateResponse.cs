using AutoMapper;
using Shared.Lib.Mapping;
using Shared.Lib.Interfaces;
using System.Text.Json.Serialization;
using Base.Domain.AggregatesModel.UserAggregate;

namespace Base.API.Application.Users.Domain.DTO;

public class AuthenticateResponse : IDTO, IMapFrom {

	public int Id { get; set; }
	public string? Name { get; set; }
	public string? LastName { get; set; }
	public string? AccessToken { get; set; }
	public DateTime AccessTokenExpiresAt { get; set; }
	public string? RefreshToken { get; set; }

	[JsonIgnore]
	public DateTime RefreshTokenExpiresAt { get; set; }

	public void Mapping(Profile profile) {
		profile.CreateMap<User, AuthenticateResponse>().ReverseMap();
	}
}


