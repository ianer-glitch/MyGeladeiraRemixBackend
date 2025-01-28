using System.Collections.Immutable;
using Extensions;
using Fridge.Application.UseCases.Fridge.AddItem;
using Fridge.Application.UseCases.Fridge.GetItem;
using Fridge.Application.UseCases.Item.Create;
using Fridge.Application.UseCases.Item.Get;
using Fridge.Domain.Fridges.AddItem;
using Fridge.Domain.Fridges.GetItem;
using Fridge.Domain.Items.Create;
using Fridge.Domain.Items.Get;
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

builder.Services.AddScoped(typeof(IFileAdapter<>), typeof(FileAdapter<>));
builder.Services.AddScoped<IFileAdapterResult,FileAdapterResult>();

builder.Services.AddScoped<IGetItems, GetItems>();

builder.Services.AddScoped<IAddItemsToFridge, AddItemsToFridge>();

builder.Services.AddScoped<IGetFridgeItems, GetFridgeItems>();
builder.Services.AddSwaggerConfiguration();


var app = builder.Build();

app.ApplyMigrations<FridgeContext>();
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