using Models;

namespace Fridge.Domain.ShoppingLists;

public class ShoppingList : Entity
{
    public ShoppingList(Guid userId): base(userId)
    {
        UserId = userId;
    }
    public ShoppingList()
    {
        
    }
    
    public Guid UserId { get; set; }    
    
    
}