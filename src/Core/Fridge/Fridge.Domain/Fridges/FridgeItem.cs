using System.ComponentModel.DataAnnotations.Schema;
using Fridge.Domain.Items;
using Fridge.Domain.ShoppingLists;

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
        double itemWeight,
        Guid fridgeId,
        Guid userInclusionId
        ) : base(name,
                                    color,
                                    expiration,
                                    minimunQuantity, 
                                    quantity, 
                                    iconName,
                                    itemWeight,
                                    userInclusionId)
    {
        FridgeId = fridgeId;
    }
    
    [ForeignKey("FridgeId")]
    public Guid FridgeId { get; set; }
    public virtual Fridge Fridge { get; set; }
    
    [ForeignKey("ShoppingListId")]
    public Guid? ShoppingListId { get; set; }
    public virtual ShoppingList ShoppingList { get; set; }  
}