using Statistic.Domain.Statistics.GetByUser;

namespace Statistic.Application.Statistics.GetByUser;

public class GetStatisticByUserIn : IGetStatisticByUserIn
{
    public Guid UserId { get; set; }
}