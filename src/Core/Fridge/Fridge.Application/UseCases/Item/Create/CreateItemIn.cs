using Fridge.Domain.Items.Create;
using Microsoft.AspNetCore.Http;

namespace Fridge.Application.UseCases.Item.Create;

public class CreateItemIn : ICreateItemIn
{
    public required string Color { get; set; }
    public required string Name { get; set; }
    public int MinimumQuantity { get; set; }
    public int DefaultQuantity { get; set; }
    public DateTime Expiration { get; set; }
    public required IFormFile Icon { get; set; }
    public Guid UserCreationId { get; set; }
}