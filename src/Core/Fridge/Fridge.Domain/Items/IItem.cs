namespace Fridge.Domain.Items;

public interface IItem
{
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime Expiration { get; set; }
    
    public string IconName { get; set; } 
    public int MinimunQuantity { get; set; }    
    public int Quantity { get; set; }
    public double Weight { get; set; }
    public bool ShouldAddToShoppingList { get;  }
    public bool IsExpired { get; }
    string GetPercentageExpired();
    void SetName(string name);
    void SetColor(string color);

    void SetExpiration(DateTime expiration);

    void SetWeight(double weight);


}