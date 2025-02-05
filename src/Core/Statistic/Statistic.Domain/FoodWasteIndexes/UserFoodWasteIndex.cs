using Models;

namespace Statistic.Domain.FoodWasteIndexes;

public class UserFoodWasteIndex : Entity
{
    public UserFoodWasteIndex(double index,Guid userId) : base(userId)
    {
        Index = index;
    }
    public UserFoodWasteIndex()
    {
        
    }

    public double Index { get; set; } 
}