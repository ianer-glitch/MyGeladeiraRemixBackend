namespace Fridge.Domain.Items.Update;

public interface IUpdateItemIn
{
    public Guid ItemId { get; set; }    
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime Expiration { get; set; }
    
    public string IconName { get; set; } 
    public int MinimunQuantity { get; set; }    
    public int Quantity { get; set; }
}