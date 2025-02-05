namespace Statistic.Domain.Statistics.GetByUser;

public interface IGetStatisticByUserOut
{
    public double NationalFoodWasteIndex { get; set; }
    public double UserFoodWasteIndex { get; set; }
}