using MediatR;
using System.Text;
using System.Reflection;
using Shared.Lib.Models;
using Shared.Lib.Services;
using Shared.Lib.Middlewares;
using Shared.Lib.Helpers.Validator;
using Shared.Lib.Services.CurrentUser;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Base.API.Application.Users.Services;
using Base.API.Application.Clients.Services;
using Base.Domain.AggregatesModel.UserAggregate;
using Base.API.Application.Common.Services;
using Base.API.Application.SavingAccounts.Services;
using Base.API.Application.Transactions.Services;
using Base.Domain.AggregatesModel.ClientAggregate;
using Base.Domain.AggregatesModel.SavingAccountAggregate;
using Base.Domain.AggregatesModel.TransactionAggregate;
using Base.Infrastructure.Persistence.Repositories;
using Base.Infrastructure.Persistence;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins, policy  => {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers().AddFluentValidation(options => {
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddAuthentication(option => {
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {
	options.TokenValidationParameters = new TokenValidationParameters {
		ValidateIssuer           = false,
		ValidateAudience         = false,
		ValidateLifetime         = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer              = builder.Configuration["AppSettings:Issuer"],
		ValidAudience            = builder.Configuration["AppSettings:Audience"],
		IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"])) //Configuration["JwtToken:SecretKey"]
	};
});

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISavingAccountService, SavingAccountService>();
builder.Services.AddScoped<ISavingAccountRepository, SavingAccountRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped(typeof(IValidatorHelper<>), typeof(ValidatorHelper<>));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
// builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => {
	c.EnableAnnotations();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
	var dataContext = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
	dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();