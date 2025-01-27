using System.ComponentModel.DataAnnotations.Schema;
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
        Guid userInclusionId,
        Guid FridgeId) : base(name,
                                    color,
                                    expiration,
                                    minimunQuantity, 
                                    quantity, 
                                    iconName,
                                    userInclusionId)
    {
        
    }
    
    [ForeignKey("FridgeId")]
    public Guid FridgeId { get; set; }
    public virtual Fridge Fridge { get; set; }  
}