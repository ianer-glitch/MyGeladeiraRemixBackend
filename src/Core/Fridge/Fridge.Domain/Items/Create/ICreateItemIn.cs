using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fridge.Domain.Items.Create;

public interface ICreateItemIn
{
    public string Color { get; set; }
    public string Name { get; set; }
    public int MinimumQuantity { get; set; }
    public int Quantity { get; set; }
    public float Weight { get; set; }
    public DateTime Expiration { get; set; }
    public IFormFile Icon { get; set; }
    public Guid UserCreationId { get; set; }    
    
}