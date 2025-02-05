using Statistic.Domain.Statistics.GetByAllUser;

namespace Statistic.Application.Statistics.GetByAllUser;

public class GetStatisticByAllUserOut : IGetStatisticByAllUserOut
{
    public double NationalFoodWasteIndex { get; set; }
    public double UserFoodWasteIndex { get; set; }
}