using Extensions;
using Fridge.Application.UseCases.Item.Create;
using Fridge.Domain.Items.Create;
using Fridge.Domain.Ports.FileAdapter;
using Fridge.Infrastructure;
using Minio.Adapter;
using Ports;
using Postgre.Adapter;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FridgeContext>(builder.Configuration,"Database");
builder.Services.ConfigureJwtAuth(builder.Configuration);

builder.Services.AddScoped(typeof(IRepository<,>),typeof(Repository<,>)); 
builder.Services.AddScoped<ICreateItem, CreateItem>();
builder.Services.AddScoped<IFileAdapter<FileAdapterResult>, FileAdapter<FileAdapterResult>>();
builder.Services.AddSwaggerConfiguration();


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