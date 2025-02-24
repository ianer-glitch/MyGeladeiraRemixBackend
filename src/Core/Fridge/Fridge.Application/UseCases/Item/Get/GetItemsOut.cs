namespace Fridge.Application.UseCases.Item.Get;

public class GetItemsOut : IGetItemsOut
{
    public Guid Id { get; set; }
    public required string Color { get; set; }
    public required string Name { get; set; }
    public required string Icon { get; set; }
    public int MinimumQuantity { get; set; }
    public int Quantity { get; set; }
    public DateTime Expiration { get; set; }
    public double Weight { get; set; }

    
  
}

