using Models;

namespace Statistic.Domain;

public class UserFoodWasteIndex : Entity
{
    public UserFoodWasteIndex(float userIndex,Guid userId) : base(userId)
    {
        
    }
    public UserFoodWasteIndex()
    {
        
    }
    
    public float UserIndex { get; set; }
}