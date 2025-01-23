using Fridge.Domain.Items.Create;

namespace Fridge.Application.UseCases.Item.Create;

public class CreateItemOut : ICreateItemOut
{
    public Domain.Items.Item Item { get; set; }
}