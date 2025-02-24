using Fridge.Domain.Items.Update;

namespace Fridge.Application.UseCases.Item.Update;

public class UpdateItemIn: IUpdateItemIn
{
    public Guid ItemId { get; set; }
    public  string Color { get; set; }
    public  string Name { get; set; }
    public int MinimumQuantity { get; set; }
    public int Quantity { get; set; }
    public double Weight { get; set; }
    public DateTime Expiration { get; set; }
    public IFormFile? Icon { get; set; }
    public Guid? UserCreationId { get; set; }
}