namespace Fridge.Domain.Fridges.GetItem;

public interface IGetFridgeItemsOut
{
    public string IconLink { get; set; }
    public string Color { get; set; }
    public string PercentageExpired { get; set; }
    public int Count { get; set; }
    public Guid ItemId { get; set; }    
}