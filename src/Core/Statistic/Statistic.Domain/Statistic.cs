using Models;

namespace Statistic.Domain;

public class Statistic : Entity
{
    public Statistic()
    {
        
    }
    public Statistic(Guid userId) : base(userId)    
    {
        
    }
}