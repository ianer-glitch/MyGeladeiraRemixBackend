using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using User.Application.UseCases;
using User.Infrastructure;
using Extensions;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;
using GreeterUseCase = User.Application.UseCases.GreeterUseCase;
using UserUseCase = User.Application.UseCases.UserUseCase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<UserContext>(builder.Configuration, "Database");
builder.Services.AddIdentity<User.Domain.Models.User,UserRoles>()
    .AddEntityFrameworkStores<UserContext>()   
    .AddDefaultTokenProviders();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterUseCase>();
app.MapGrpcService<UserUseCase>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();