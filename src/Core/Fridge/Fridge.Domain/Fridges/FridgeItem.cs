using Fridge.Domain.Items;

namespace Fridge.Domain.Fridges;

public class FridgeItem : Item
{
    public FridgeItem()
    {
        
    }

    public FridgeItem(
        string name,
        string color,
        DateTime expiration,
        int minimunQuantity ,
        int quantity ,
        string iconName,
        Guid userInclusionId) : base(name,
                                    color,
                                    expiration,
                                    minimunQuantity, 
                                    quantity, 
                                    iconName,
                                    userInclusionId)
    {
        
    }
}