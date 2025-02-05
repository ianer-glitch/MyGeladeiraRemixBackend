using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Statistic.Domain.Statistics;

public class ExpiredStatistic :Entity
{
    public ExpiredStatistic(Guid itemId,double itemWeight ,Guid statisticId , Guid userId) :base(userId)
    {
        ItemId = itemId;
        ItemWeight = itemWeight;
        StatisticId = statisticId;
    }
    public ExpiredStatistic()
    {
        
    }
    
    public Guid ItemId { get; set; }
    public double  ItemWeight { get; set; }
    
    [ForeignKey("StatisticId")]
    public Guid StatisticId { get; set; }
    public virtual Statistic Statistic { get; set; }    
}