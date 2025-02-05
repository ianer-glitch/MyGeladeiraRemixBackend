namespace Statistic.Domain.Statistics.GetByAllUser;

public interface IGetStatisticByAllUserOut
{
    public double NationalFoodWasteIndex { get; set; }
    public double UserFoodWasteIndex { get; set; }
}