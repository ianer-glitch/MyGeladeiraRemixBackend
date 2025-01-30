using Fridge.Domain.Items.Delete;

namespace Fridge.Application.UseCases.Item.Delete;

public class DeleteItemOut : IDeleteItemOut
{
    public bool Success { get; set; }
}