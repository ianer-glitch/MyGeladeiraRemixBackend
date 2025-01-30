using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using User.Application.UseCases;
using User.Infrastructure;
using Extensions;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;
using User.Service.Services;
using GreeterUseCase = User.Application.UseCases.GreeterUseCase;
using UserUseCase = User.Application.UseCases.UserUseCase;
using UserService = User.Service.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<UserContext>(builder.Configuration, "Database");
builder.Services.AddIdentity<User.Domain.Models.User,UserRoles>()
    .AddEntityFrameworkStores<UserContext>()   
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserUseCase, UserUseCase>();


var app = builder.Build();

app.ApplyMigrations<UserContext>();

using (var scope = app.Services.CreateScope())
{
    var user = builder.Configuration.GetSection("DEFAULT_ADMIN_USER").Value;
    var password = builder.Configuration.GetSection("DEFAULT_ADMIN_PASSWORD").Value;
    var userService = scope.ServiceProvider.GetRequiredService<IUserUseCase>();
    var request = new PCreateUserIn
    {
        Email = user,
        Password = password,
        BirthDate = DateTime.UtcNow.AddYears(-20).ToTimestamp(),
        FirstName = "Admin",
        LastName = "Admin",

    };
    await userService.CreateUserAsync(request);
    await userService.CreateRolesAsync();
    await userService.AddUserRoleAdministrator(request.Email!);
}

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterUseCase>();
app.MapGrpcService<UserService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();