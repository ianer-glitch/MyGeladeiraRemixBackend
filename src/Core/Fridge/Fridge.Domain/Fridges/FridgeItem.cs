using System.ComponentModel.DataAnnotations.Schema;
using Extensions;
using Fridge.Domain.Items;
using Fridge.Domain.ShoppingLists;
using Models;

namespace Fridge.Domain.Fridges;

public class FridgeItem :Entity,IItem
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
        Guid itemId,
        Guid fridgeId,
        Guid userInclusionId
        ) : base(userInclusionId)
    {
        FridgeId = fridgeId;
        ItemId = itemId;  
        SetName(name);
        SetColor(color);    
        SetExpiration(expiration);
        MinimunQuantity = minimunQuantity;
        Quantity = quantity;
        IconName = iconName;    
        SetWeight(itemWeight);
        
    }
    
    [ForeignKey("FridgeId")]
    public Guid FridgeId { get; set; }
    public virtual Fridge Fridge { get; set; }
    
    [ForeignKey("ItemId")]
    public Guid ItemId { get; set; }
    public virtual Item Item { get; set; }
    
    [ForeignKey("ShoppingListId")]
    public Guid? ShoppingListId { get; set; }
    public virtual ShoppingList ShoppingList { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime Expiration { get; set; }
    public string IconName { get; set; }
    public int MinimunQuantity { get; set; }
    public int Quantity { get; set; }
    public double Weight { get; set; }
    
    public bool ShouldAddToShoppingList => Quantity < MinimunQuantity;
    
    public bool IsExpired => Expiration > DateTime.UtcNow;
    
    public void SetIconName(string icon)
    {
        if(icon.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(icon));
        IconName = icon;    
    }
    
    public void SetName(string name)
    {
        if(name.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(name));
        Name= name;
    }

    public void SetColor(string color)
    {
        if(color.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(color));
        Color = color;
    }

    public void SetExpiration(DateTime expiration)
    {
        if(expiration < DateTime.Now)
            throw new ArgumentException("expiration should be in the future", nameof(expiration));  
        Expiration = expiration;
    }

    public void SetWeight(double weight)
    {
        if(double.IsNaN(weight) || weight <= 0)
            throw new ArgumentException("Weight must be grater than 0", nameof(weight));
        Weight = weight;
    }

    public string GetPercentageExpired()
    {
        var currentDate = DateTime.Now.Ticks;
        var exp =  Expiration.Ticks;
        
        // Calculate percentage of time passed (as a percentage)
        double result = (100 * currentDate/exp) ;
        return $"%{result}";

    }
}