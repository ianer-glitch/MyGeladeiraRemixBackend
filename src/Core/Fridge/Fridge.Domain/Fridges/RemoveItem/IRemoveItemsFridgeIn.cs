namespace Fridge.Domain.Fridges.RemoveItem;

public interface IRemoveItemsFridgeIn
{
    public Guid UserId { get; set; }
    public IEnumerable<Guid> FridgeItemIds { get; set; }
    
}