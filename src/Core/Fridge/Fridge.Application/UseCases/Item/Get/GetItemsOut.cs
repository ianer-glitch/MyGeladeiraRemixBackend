using Fridge.Domain.Items.Get;

namespace Fridge.Application.UseCases.Item.Get;

public class GetItemsOut : IGetItemsOut
{
    public Guid Id { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}

