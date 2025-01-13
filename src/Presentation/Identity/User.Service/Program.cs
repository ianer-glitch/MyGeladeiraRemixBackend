using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using User.Application.UseCases;
using User.Infrastructure;
using GreeterUseCase = User.Application.UseCases.GreeterUseCase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddIdentity<User.Domain.Models.User,IdentityRole>()
    .AddEntityFrameworkStores<UserContext>()   
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterUseCase>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();