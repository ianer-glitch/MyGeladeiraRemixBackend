namespace Fridge.Domain.Fridges.GetRecommendedItem;

public interface IGetRecommendedItemsIn
{
    public Guid UserId { get; set; }
    public  string  ResponseLanguage { get; set; }
}