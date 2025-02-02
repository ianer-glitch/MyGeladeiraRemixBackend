using Extensions;
using Ports;
using Postgre.Adapter;
using RabbitMq.Adapter;
using Statistic.Application.Statistics.CreateExpired;
using Statistic.Domain.Statistics.Create;
using Statistic.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddDbContext<StatisticContext>(builder.Configuration,"Database");
builder.Services.ConfigureJwtAuth(builder.Configuration);
builder.Services.AddScoped(typeof(IRepository<,>),typeof(Repository<,>));
builder.Services.AddScoped<IListenObjectsFromQueue, ListenObjectsFromQueue>();
builder.Services.AddHostedService<CreateExpiredStatistic>();

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