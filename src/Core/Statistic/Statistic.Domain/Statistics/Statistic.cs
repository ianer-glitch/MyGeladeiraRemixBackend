using Models;

namespace Statistic.Domain.Statistics;

public class Statistic : Entity
{
    public Statistic()
    {
        
    }
    public Statistic(Guid userId) : base(userId)    
    {
        
    }
}