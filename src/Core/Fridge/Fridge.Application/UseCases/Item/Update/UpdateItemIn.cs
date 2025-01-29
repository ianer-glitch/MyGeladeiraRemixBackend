using Fridge.Domain.Items.Update;

namespace Fridge.Application.UseCases.Item.Update;

public class UpdateItemIn: IUpdateItemIn
{
    public Guid ItemId { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required string IconName { get; set; }
    public DateTime Expiration { get; set; }
    public int MinimunQuantity { get; set; }
    public int Quantity { get; set; }
}