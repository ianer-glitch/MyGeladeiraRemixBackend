namespace Fridge.Domain.Fridges.UpdateItem;

public interface IUpdateFridgeItemIn
{
    public DateTime Expiration { get; set; }
    public int MinimunQuantity { get; set; }    
    public int Quantity { get; set; }
    public Guid ItemId { get; set; }
    
    public Guid UserId { get; set; }
    
}