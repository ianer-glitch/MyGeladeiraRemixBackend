namespace Fridge.Application.UseCases.Item.Get;

public class GetItemsOut : IGetItemsOut
{
    public Guid Id { get; set; }
    public required string Color { get; set; }
    public required string Name { get; set; }
    public required string Icon { get; set; }
}

