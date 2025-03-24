using Extensions;
using Grpc.Adapter;
using Identity.Application.Helpers;
using Identity.Domain.Ports;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = ServiceExtensions.GetLoggerConfiguration().CreateLogger();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IIdentityGrpcConnection, Connection>();
builder.Services.AddScoped<IConnectionHelper, ConnectionHelper>();
builder.Services.ConfigureJwtAuth(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.ConfigureCors();


var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();