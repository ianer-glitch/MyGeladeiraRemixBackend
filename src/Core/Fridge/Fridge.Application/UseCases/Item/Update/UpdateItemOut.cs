using Fridge.Domain.Items.Update;

namespace Fridge.Application.UseCases.Item.Update;

public class UpdateItemOut : IUpdateItemOut
{
    public bool Success { get; set; }
}