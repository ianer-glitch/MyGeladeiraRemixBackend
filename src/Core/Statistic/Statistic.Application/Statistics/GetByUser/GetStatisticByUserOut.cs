using Statistic.Domain.Statistics.GetByUser;

namespace Statistic.Application.Statistics.GetByUser;

public class GetStatisticByUserOut : IGetStatisticByUserOut
{
    public double NationalFoodWasteIndex { get; set; }
    public double UserFoodWasteIndex { get; set; }
}