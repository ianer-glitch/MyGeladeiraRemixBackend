namespace Fridge.Domain.Fridges.AddItem;

public interface IAddItemsToFridgeIn
{
    public Guid UserId { get; set; }    
    public IEnumerable<Guid> ItemIds { get; set; }
}