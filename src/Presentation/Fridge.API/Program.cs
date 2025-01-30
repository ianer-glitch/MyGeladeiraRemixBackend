using System.Collections.Immutable;
using Extensions;
using Fridge.Application.UseCases.Fridge.AddItem;
using Fridge.Application.UseCases.Fridge.GetItem;
using Fridge.Application.UseCases.Fridge.RemoveItems;
using Fridge.Application.UseCases.Fridge.UpdateItem;
using Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;
using Fridge.Application.UseCases.Item.Create;
using Fridge.Application.UseCases.Item.Delete;
using Fridge.Application.UseCases.Item.Get;
using Fridge.Application.UseCases.Item.Update;
using Fridge.Application.UseCases.ShoppingList.AddItems;
using Fridge.Application.UseCases.ShoppingList.GetItems;
using Fridge.Application.UseCases.ShoppingList.RemoveItems;
using Fridge.Domain.Fridges.AddItem;
using Fridge.Domain.Fridges.GetItem;
using Fridge.Domain.Fridges.RemoveItem;
using Fridge.Domain.Fridges.UpdateItem;
using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;
using Fridge.Domain.Items.Create;
using Fridge.Domain.Items.Delete;
using Fridge.Domain.Items.Get;
using Fridge.Domain.Items.Update;
using Fridge.Domain.Ports.FileAdapter;
using Fridge.Domain.ShoppingLists.AddItems;
using Fridge.Domain.ShoppingLists.GetItems;
using Fridge.Domain.ShoppingLists.RemoveItems;
using Fridge.Infrastructure;
using Minio.Adapter;
using Ports;
using Postgre.Adapter;
using RabbitMq.Adapter;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddDbContext<FridgeContext>(builder.Configuration,"Database");
builder.Services.ConfigureJwtAuth(builder.Configuration);

builder.Services.AddScoped(typeof(IRepository<,>),typeof(Repository<,>)); 
builder.Services.AddScoped(typeof(IFileAdapter<>), typeof(FileAdapter<>));
builder.Services.AddScoped<IFileAdapterResult,FileAdapterResult>();

builder.Services.AddScoped<ICreateItem, CreateItem>();
builder.Services.AddScoped<IGetItems, GetItems>();
builder.Services.AddScoped<IUpdateItem, UpdateItem>();
builder.Services.AddScoped<IDeleteItem, DeleteItem>();

builder.Services.AddScoped<IAddItemsToFridge, AddItemsToFridge>();
builder.Services.AddScoped<IGetFridgeItems, GetFridgeItems>();
builder.Services.AddScoped<IUpdateFridgeItem, UpdateFridgeItem>();
builder.Services.AddScoped<IUpdateMultipleFridgeItemsQuantities, UpdateMultipleFridgeItemsQuantities>();
builder.Services.AddScoped<IRemoveItemsFridge, RemoveItemsFridge>();

builder.Services.AddScoped<IRemoveItemsShoppingList, RemoveItemsShoppingList>();
builder.Services.AddScoped<IGetItemsShoppingList, GetItemsShoppingList>();
builder.Services.AddScoped<IAddItemsShoppingList, AddItemShoppingList>();

builder.Services.AddScoped<ISendObjectOnQueue,SendObjectOnQueue>();





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