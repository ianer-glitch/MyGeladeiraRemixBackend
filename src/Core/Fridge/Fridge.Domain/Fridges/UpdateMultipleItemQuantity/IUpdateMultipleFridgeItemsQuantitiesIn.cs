namespace Fridge.Domain.Fridges.UpdateMultipleItemQuantity;

public interface IUpdateMultipleFridgeItemsQuantitiesIn
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
}