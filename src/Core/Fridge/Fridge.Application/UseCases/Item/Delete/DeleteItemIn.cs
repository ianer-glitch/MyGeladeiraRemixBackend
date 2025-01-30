using Fridge.Domain.Items.Delete;

namespace Fridge.Application.UseCases.Item.Delete;

public class DeleteItemIn :IDeleteItemIn
{
    public Guid ItemId { get; set; }
}