using Grpc.Adapter;
using Identity.Domain.Ports;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var userServiceUrl = builder.Configuration.GetSection("ConnectionStrings").GetSection("UserService").Value ?? string.Empty;
ArgumentException.ThrowIfNullOrEmpty(userServiceUrl);
builder.Services.AddScoped<IIdentityGrpcConnection>(provider => new Connection(userServiceUrl));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();