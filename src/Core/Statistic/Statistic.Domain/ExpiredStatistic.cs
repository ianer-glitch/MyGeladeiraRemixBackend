using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Statistic.Domain;

public class ExpiredStatistic :Entity
{
    public ExpiredStatistic(Guid itemId,float itemWeight ,Guid statisticId , Guid userId) :base(userId)
    {
        ItemId = itemId;
        ItemWeight = itemWeight;
        StatisticId = statisticId;
    }
    public ExpiredStatistic()
    {
        
    }
    
    public Guid ItemId { get; set; }
    public float ItemWeight { get; set; }
    
    [ForeignKey("StatisticId")]
    public Guid StatisticId { get; set; }
    public virtual Statistic Statistic { get; set; }    
}