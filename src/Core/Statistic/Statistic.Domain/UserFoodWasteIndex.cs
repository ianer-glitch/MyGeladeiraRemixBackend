using Models;
using Statistic.Domain.ValueObjects;

namespace Statistic.Domain;

public class UserFoodWasteIndex : Entity
{
    public UserFoodWasteIndex(float userIndex,Guid userId) : base(userId)
    {
        
    }
    public UserFoodWasteIndex()
    {
        
    }

    public float Index { get; set; } 
}