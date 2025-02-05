using System.Diagnostics.CodeAnalysis;
using Extensions;
using Models;

namespace Fridge.Domain.Items;

public class Item : Entity 
{
    public Item()
    {
        
    }

    public Item(string name,
                string color,
                DateTime expiration,
                int minimunQuantity ,
                int quantity ,
                string iconName,
                double weight,
                Guid userInclusionId):base(userInclusionId)
    {
        SetName(name);
        SetColor(color);    
        SetExpiration(expiration);
        MinimunQuantity = minimunQuantity;
        Quantity = quantity;
        IconName = iconName;    
        SetWeight(weight);
    }
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